/*
{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Modified by Tim Brassey from https://www.shadertoy.com/view/mdtXzH by Zygal and others",
    "DESCRIPTION": "Lava Blobs",    
	"CATEGORIES": ["Automatically Converted",],
    "IMPORTED": {
},
    "INPUTS": [


    { "LABEL": "Blobiness", "NAME": "One", "TYPE": "float", "MIN": 0.9, "MAX": 1.05, "DEFAULT": 1 },
    { "LABEL": "Outline", "NAME": "Two", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.5 },
    { "LABEL": "Brightness", "NAME": "Three", "TYPE": "float", "MIN": 0.5, "MAX": 1, "DEFAULT": 1},
    { "LABEL": "Speed", "NAME": "Four", "TYPE": "float", "MIN": 1, "MAX": 100, "DEFAULT": 50},
    { "LABEL": "Blob Red", "NAME": "Five", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 1},
    { "LABEL": "Blob Green", "NAME": "Six", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0},
    { "LABEL": "Blob Blue", "NAME": "Seven", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0},
    { "LABEL": "Back Red", "NAME": "Eight", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0},
    { "LABEL": "Back Green", "NAME": "Nine", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0},
    { "LABEL": "Back Blue", "NAME": "Ten", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0},
    { "LABEL": "Brightness", "NAME": "Eleven", "TYPE": "float", "MIN": 0.0, "MAX": 5, "DEFAULT": 2.5},
    { "LABEL": "Diffusion", "NAME": "Twelve", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0},
 
    ]
}

*/



#define PI 3.1415926535897932384626433832795

const int MAX_ITER = 100;
const float LIMIT = 0.001;//how close the ray have to be to trigger

struct Material {
    vec3 color;
    float specular;
    float diffuse;
    float ambient;
    float shininess;
};
struct Surface{
    float depth;
    Material mat;
};

struct Final{
    Surface s;
    int iterations;
};

mat3x3 rotate(vec3 angle) {
    return mat3(
    cos(angle.y)*cos(angle.z), cos(angle.z)*sin(angle.x)*sin(angle.y)-cos(angle.x)*sin(angle.z), cos(angle.x)*cos(angle.z)*sin(angle.y)+sin(angle.x)*sin(angle.z),
    cos(angle.y)*sin(angle.z), cos(angle.x)*cos(angle.z)+sin(angle.x)*sin(angle.y)*sin(angle.z), -cos(angle.z)*sin(angle.x)+cos(angle.x)*sin(angle.y)*sin(angle.z),
    -sin(angle.y), cos(angle.y)*sin(angle.x), cos(angle.x)*cos(angle.y));
}

Surface opSmoothUnion(Surface d1, Surface d2, float k) {
    float d = clamp(0.5 + 0.5*(d2.depth-d1.depth)/k, 0.0, One);
    Material material = Material(mix(d2.mat.color, d1.mat.color, d), mix(d2.mat.specular, d1.mat.specular, d), mix(d2.mat.diffuse, d1.mat.diffuse, d), mix(d2.mat.ambient, d1.mat.ambient, d), mix(d2.mat.shininess, d1.mat.shininess, d));
    Surface s = Surface(mix(d2.depth, d1.depth, d) - k*d*(1.0-d), material);
    return s;
}

Surface sfJoin(Surface d1, Surface d2) {
    if(d1.depth < d2.depth)
    return d1;
    else
    return d2;
}

Surface sdSphere(vec3 p, float r, vec3 offset, Material mat)
{
    return Surface(length(p-offset)-r, mat);
}

// bounce the balls of the walls
vec3 position(float xSpeed, float ySpeed, float zSpeed, float time) {
    float x = (2./PI)*(asin(sin((2.*PI/xSpeed) * time)));
    float y = (1.5/PI)*(asin(sin((2.*PI/ySpeed) * time)));
    float z = (1./PI)*(asin(sin((2.*PI/zSpeed) * time)));
    return vec3(x, y, z);
}

/**
* add all objects here
*/
Surface sdScene(vec3 p) {
    float time = TIME/Four;
    Surface d = Surface(100., Material(vec3(0.), 0., 0., 0., 0.));
    //color, specular, diffuse, ambient, shininess
    Material sphere1 = Material(vec3(Five, Six, Seven), 0.25, 0.5, 0.15, 10.);
    for (float  i = 0.; i < 15.; i++){
        vec3 po = position(float(.4 / sin(i)), float(.3 / sin(i)), float(.2 / sin(i)), time) * 5.;
        d = opSmoothUnion(d, sdSphere(p, 1., po, sphere1), 1.);
    }
    return d;
}

vec3 normal(vec3 p){ //finds the normal of a point on a surface
    vec2 e = vec2(1.0, -1.0) * 0.0005;// epsilon
    return normalize(
    e.xyy * sdScene(p + e.xyy).depth +
    e.yyx * sdScene(p + e.yyx).depth +
    e.yxy * sdScene(p + e.yxy).depth +
    e.xxx * sdScene(p + e.xxx).depth);
}
/**
* o = ray origin
* d = ray direction
*/
Final rayMarch(vec3 o, vec3 d){
    Surface sur;
    Surface sc;
    int iter = 0;
    for (int i = 0; i < MAX_ITER; i++){
        sc = sdScene(o + d*sur.depth);
        sur.depth += sc.depth;
        sur.mat = sc.mat;
        if (sc.depth < LIMIT || sur.depth > 100.){
            iter = (i);
            break;
        }
    }
    return Final(sur, iter);
}
/**
* finds if an area should be in shadow
* p: the ray intersection point
* lPos: the location of the light source (not direction)
* intensity: Normalized value of how dark the shadows should be, 1 = light, 0 = black
*/
float shadowMarch(vec3 p, vec3 lPos, float intensity){
    float depth = LIMIT * 2.;
    Surface sc;

    vec3 dir = normalize(lPos - p);//the direction to the lightsource

    //checks so it doesnt count its own surface
    sc = sdScene(p + depth * dir);
    depth += sc.depth;
    if (sc.depth < LIMIT) return 1.;

    for (int i = 1; i < MAX_ITER; i++) {
        sc  = sdScene(p + depth * dir);
        depth += sc.depth;
        if (sc.depth < LIMIT){
            return 1.-intensity;
        }
    }
    return 1.;
}
/**
* Calculates how light bounces on the surface
* p: the intersect point
* lPos: The light location (not direction)
* N: the normal at p
* RD: ray direction, from camea
* m: the surface material
*/
vec3 phongLightning(vec3 p, vec3 lPos, vec3 N, vec3 RD, Material m){
    vec3 L = normalize(lPos - p);
    vec3 ambient = m.color * m.ambient * Eleven;
    vec3 diffuse = m.diffuse * clamp(dot(L, N), Twelve, 1.) * m.color;
    vec3 specular = m.specular * pow(clamp(-dot(reflect(L, N), -RD), 0., 1.), m.shininess) * vec3(1.);
    specular = vec3(max(specular.x, 0.), max(specular.y, 0.), max(specular.z, 0.));//removes any negative specular values

    return ambient + diffuse + specular;
}

vec4 materialColorForPixel(vec2 texCoord)
 {

    vec2 uv = gl_FragCoord.xy / RENDERSIZE.xy - 0.5;
    uv.x = uv.x * RENDERSIZE.x / RENDERSIZE.y;//make screen square
    //camera and lgiht
    vec3 rayOrigin = vec3(1.0, 1.0, 5.);
    vec3 rayDir = normalize(vec3(uv, -1.));
    vec3 light = vec3(cos(TIME) * 10., 10., sin(TIME) * 10.);//the direction of the light source
    Final f = rayMarch(rayOrigin, rayDir);//finds the surface that the ray intersects
    Surface d = f.s;//the surface that the ray intersects
    if (d.depth < 100.){ //if the object is close enough to not be sky, basicly render distance, if changed, it must also be changed in raymarch
        vec3 p = rayOrigin + rayDir * d.depth;//where the ray hit an object
        vec3 N = normal(p);//the normal vector at the ray intersection
        vec3 col = phongLightning(p, light, N, rayDir, d.mat);//applices light and shadow
        return vec4(col, Three);
    } else {
        vec3 col = vec3(0.) + vec3(Eight, Nine, Ten) * float(f.iterations);//background color
        return vec4(col, Two); //background color
    }
}