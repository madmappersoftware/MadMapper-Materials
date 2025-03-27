/*{
    "CREDIT": "1024 architecture\nFranz",
    "DESCRIPTION": "Inspired by Andy Smith - Ladders, Rocks & Lightning Swords / 2012",
    "TAGS": "album cover",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Quality", "NAME": "mat_quality", "TYPE": "int", "MIN": 10, "MAX": 200, "DEFAULT": 200 },
		{ "LABEL": "Enable Reflection", "NAME": "mat_do_rfl", "TYPE": "bool",  "DEFAULT": 1,"FLAGS":"button" }, 
		{ "LABEL": "animate", "NAME": "mat_do_anim", "TYPE": "bool",  "DEFAULT": 0,"FLAGS":"button" }, 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 3, "DEFAULT": 0.0 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 2.0, "DEFAULT": 0.35 },
		{ "LABEL": "Fov", "NAME": "mat_fov", "TYPE": "float", "MIN": 0.3, "MAX": 4.0, "DEFAULT": 1.00 },
		{ "LABEL": "Roll", "NAME": "mat_roll", "TYPE": "float", "MIN": -1., "MAX": 1.0, "DEFAULT": 0. },
		{ "LABEL": "water_height", "NAME": "mat_waterheight", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.176 },
	    { "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 2.0, -0.2 ], "MIN": [ -2.0, -2 ], "DEFAULT": [ -0.43, -0.25 ] },
		{ "LABEL": "Cloud/Density", "NAME": "mat_cloud_density", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 }, 
		{ "LABEL": "Cloud/Scale X", "NAME": "mat_cloud_x", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1. },
		{ "LABEL": "Cloud/Scale Y", "NAME": "mat_cloud_y", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1. }, 
		{ "LABEL": "Cloud/Exp", "NAME": "mat_cloud_exp", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 2. }, 
		{ "LABEL": "Cloud/Density", "NAME": "mat_density", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.7 }, 
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],
	  "IMPORTED": [
        {"NAME": "tex_graynoise", "PATH": "graynoisesmall.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

const vec2 res = vec2(1080.);
const float precis = 0.001;

const vec3 up = vec3(0.0, 1.0, 0.0);
const vec3 sun = normalize(vec3(0.0, .1, 1.0));
const vec3 horizonColor = vec3(140, 175, 161) / 255.;
const vec3 seaColor = vec3(57, 63, 89)/255.;

const float MAX_DISTANCE = 400.;

float waveHeight1 = 0.005;
float waveHeight2 = 0.004;
float waveHeight3 = 0.001;

//////// Utility
mat2 rot(float a) {
  float ca = cos(a);
  float sa = sin(a);

  return mat2(ca, sa, -sa, ca);  
}

vec2 rotate2d(vec2 v, float a) {
	float s = sin(a);
	float c = cos(a);
	mat2 m = mat2(c, -s, s, c);

	return m * v;
}

float mountNoise(in vec3 p){
	return fBm(p);
}

float volcanoNoise(in vec3 p){
	float theta = atan(p.x, -p.z);
	float h = 1. -clamp(p.y, 0., 1.);

 	return billowedNoise(vec2(theta * 5. + worleyNoise(vec2(p.y * 20., theta)) * 0.3 * h, 2.345));
}


float maskVillage(in vec3 p){
	p.y += flowNoise(p.xz, 2.3) * 0.15;
	float h0 = smoothstep(0.14, 0.15, p.y);
	float h1 = 1. - smoothstep(0.2, 0.21, p.y);
	float h = h0 * h1;

	float large_mask = smoothstep(0.4, 0.45, pow(vnoise(p.xz * 0.35 + vec2(1.5, 1.9)) + 0.4, 10.));

	return h * large_mask;
}

float village(in vec3 p){
	ivec2 tex_size = textureSize(tex_graynoise, 0);
	float v = texelFetch(tex_graynoise, ivec2(abs(p.xz) * 45.) % tex_size, 0).r;
	
	v = step(0.85, v);
	v *= maskVillage(p);

	return v;
}

float waterHeight(vec3 p) {
	p *= vec3(2., 1., 10. - mat_animation_time * 0.1);

	return flowNoise(p.xz + flowNoise(p.xz * 4.34, 3.45) * 0.06, mat_animation_time * 0.1) * 0.003;
}

vec2 sdPlane( vec3 p )
{
	return vec2(p.y - waterHeight(p) * 5. * mat_waterheight, 2.);
}

vec2 sdMount( vec3 p )
{
	float height = 1. -clamp(length(p.xz * 0.3), 0., 1.);
	float mask = step(0.001, height);
	float n = mountNoise(p) * 0.15;

	float v_top = clamp(length(p.xz), 0., 1.);
	float n_volcano = volcanoNoise(p) * 0.1;

	float fine_n = texture(tex_graynoise, p.xz * 5.).r * 0.003;
	float crater = (1. -clamp(length(p.xz * 2.2),0.,1.)) * 0.5;

	return vec2(p.y + 0.2 - height * 1.8 - n*mask - n_volcano * mask * v_top - fine_n * (1-n) + crater, 3.1);
}


vec2 map( vec3 p )
{	
	vec2 d1 = sdPlane(p);
	vec2 d2 = sdMount(p);	

	if(d2.x < d1.x) d1 = d2;

	return d1;
}

vec2 intersect( in vec3 ro, in vec3 rd )
{
	float h=precis*2.0;
    vec3 c;
    float t = 0.0;
	float maxd = MAX_DISTANCE;
    float sid = -1.0;

    for( int i=0; i < mat_quality; i++)
    {
        if(abs(h) < precis|| t > maxd ) break;
        
		t += h;
	    vec2 res = map(ro + rd * t);
        h = res.x;
	    sid = res.y;
    }

    if( t>maxd ) sid=-1.0;

    return vec2( t, sid );
}

vec3 calcNormal( in vec3 pos )
{
    vec3 eps = vec3(precis, 0.0, 0.0);
    vec3 nor;

    nor.x = map(pos+eps.xyy).x - map(pos-eps.xyy).x;
    nor.y = map(pos+eps.yxy).x - map(pos-eps.yxy).x;
    nor.z = map(pos+eps.yyx).x - map(pos-eps.yyx).x;

    return normalize(nor);
}

float calcAO( in vec3 pos, in vec3 nor )
{
	float occ = 0.0;
    float sca = 1.0;

    for( int i=0; i<5; i++ )
    {
        float hr = 0.01 + 0.12 * float(i)/4.0;
        vec3 aopos =  nor * hr + pos;
        float dd = map(aopos).x;
        occ += -(dd-hr) * sca;
        sca *= 0.95;
    }

    return clamp(1.0 - 3.0 * occ, 0.0, 1.0 );    
}


///////////
// COLORS
const vec4 cHashA4 = vec4 (0., 1., 57., 58.);
const vec3 cHashA3 = vec3 (1., 57., 113.);
const float cHashM = 43758.54;

vec4 Hashv4f (float p)
{
	return fract (sin(p + cHashA4) * cHashM);
}

float Noisefv2 (vec2 p)
{
	vec2 i = floor(p);
	vec2 f = fract(p);
	f = f * f * (3. - 2. * f);
	vec4 t = Hashv4f (dot (i, cHashA3.xy));

	return mix(mix (t.x, t.y, f.x), mix (t.z, t.w, f.x), f.y);
}

vec4 GrndCol (vec3 p, vec3 n)
{
	const vec3 gCol1 = vec3 (0.6, 0.7, 0.7), gCol2 = vec3 (0.2, 0.1, 0.1),
    gCol3 = vec3 (0.4, 0.3, 0.3), gCol4 = vec3 (0.1, 0.2, 0.1),
    gCol5 = vec3 (0.7, 0.7, 0.8), gCol6 = vec3 (0.05, 0.3, 0.03),
    gCol7 = vec3 (0.1, 0.08, 0.);

  	vec2 q = p.xz;
  	float f, d;
  	float cSpec = 0.;

  	f = 0.5 * (clamp(Noisefv2 (0.1 * q), 0., 1.) + 0.8 * Noisefv2 (0.2 * q + 2.1 * n.xy + 2.2 * n.yz));

  	vec3 col = f * mix (f * gCol1 + gCol2, f * gCol3 + gCol4, 0.65 * f);
  
	if (n.y < 0.5) {
    		f = 0.4 * (Noisefv2 (0.4 * q + vec2 (0., 0.57 * p.y)) + 0.5 * Noisefv2 (6. * q));
    		d = 4. * (0.5 - n.y);
    		col = mix (col, vec3 (f), clamp (d * d, 0.1, 1.));
    		cSpec += 0.1;
  	}

  	if (p.y > 22.) {
    		if (n.y > 0.25) {
      		f = clamp (0.07 * (p.y - 22. - Noisefv2 (0.2 * q) * 15.), 0., 1.);
      		col = mix (col, gCol5, f);
      		cSpec += f;
    		}
  	} else {
    		if (n.y > 0.45) {
      		vec3 c = (n.y - 0.3) * (gCol6 * vec3 (Noisefv2 (0.4 * q),
        		Noisefv2 (0.34 * q), Noisefv2 (0.38 * q)) + vec3 (0.02, 0.1, 0.02));
      		col = mix (col, c, smoothstep (0.45, 0.65, n.y) * (1. - smoothstep (15., 22., p.y - 1.5 + 1.5 * Noisefv2 (0.2 * q))));
    		}

    		if (p.y < 0.65 && n.y > 0.4) {
      		d = n.y - 0.4;
      		col = mix (col, d * d + gCol7, 2. * clamp ((0.65 - p.y - 0.35 * (Noisefv2 (0.4 * q) + 0.5 * Noisefv2 (0.8 * q) + 0.25 * Noisefv2 (1.6 * q))), 0., 0.3));
      		cSpec += 0.1;
    		}
 	}

	return vec4 (col, cSpec);
}


vec3 mountColor(in vec3 p, in vec3 nor, in vec3 rd){
	float n = mountNoise(p);
	float volcano_rails = pow(volcanoNoise(p), 2.2) - 0.5;
	float ambi = 0.5;
	float v_top = clamp(length(p.xz), 0., 1.);

	float lum = ambi + volcano_rails * 0.4 + n * 0.5;
	lum *= v_top;

	vec3 n_c = GrndCol(p, nor).xyz;
	vec3 col = vec3(lum) * n_c * 2.;

	//// lighting
	vec3 lp = vec3(1000.,200.,-10);
    vec3 ld = lp - p; // Light direction vector.
    float lDist = max(length(ld), 0.001); // Light to surface distance.
    ld /= lDist; // Normalizing the light vector.

    // Attenuating the light, based on distance.
    float atten = 1.5/(1. + lDist * 0.001 + lDist * lDist * 0.0001);

    // Standard diffuse term.
    float diff = abs(dot(ld, nor));
    diff = pow(diff, 2.)*.66 + pow(diff, 4.) * .34;

    // Standard specualr term.
    float spec = pow(abs(dot(reflect(-ld, nor), -rd)), 2.);
    float fres = clamp(1. + dot(rd, nor), 0., 1.);

	col += (vec3(diff + fres * fres + spec)-0.5) * 0.4;

	float h = clamp(p.y + 0.3 ,0., 1.);
	h = 1. - h;
	
	col -= h * 0.2;	

	vec3 f_c = rgb2hsv(col);	
	vec3 ff_c = hsv2rgb(vec3(f_c.x, f_c.y * 0.5, f_c.z));	

	ff_c = ff_c*vec3(1.2, 0.9, 0.6);

	ff_c += vec3(village(p) * 0.85);

	return  ff_c;
}

vec3 waterColor(in vec3 p){
	float r = 1. -clamp(length(p.xz * 0.1), 0., 1.);
	vec3 col = mix(seaColor, horizonColor, vec3(r));
	
	return col;
}


//////////////////////////
// CLOUDS

#define MIN_HEIGHT 20.0
#define MAX_HEIGHT 40.5
#define WIND vec2(0.2, 0.1)

vec3 sundir = normalize(vec3(1.0, 0.75, 1.0));

float noisee( in vec3 x )
{
	return billowedNoise(x);
}

float fractal_noise(vec3 p)
{
    float f = 0.0;

    // add animation
	p.xz *= vec2(mat_cloud_x, mat_cloud_y) * 0.01;
    p = p + vec3(1.0, 0.1, 0.0) * mat_animation_time * 0.1;
    p = p * 3.0;
    f += 0.50000 * noisee(p); p = 2.0 * p;
	f += 0.25000 * noisee(p); p = 2.0 * p;
	f += 0.12500 * noisee(p); p = 2.0 * p;
	f += 0.06250 * noisee(p); p = 2.0 * p;
    f += 0.03125 * noisee(p);
    
    return f;
}

float density(vec3 pos)
{    
    float den = 10.*mat_cloud_density * fractal_noise(pos * 0.3) - 2.0 + (pos.y - MIN_HEIGHT);
    float edge = (0.49+mat_density) - smoothstep(MIN_HEIGHT, MAX_HEIGHT, pos.y);

    edge *= edge;
    den *= edge;
    den = clamp(den, 0.0, 1.0);
    
    return den;
}

vec3 raymarching(vec3 ro, vec3 rd, float t, vec3 backCol)
{   
    vec4 sum = vec4(0.0);
    vec3 pos = ro + rd * t;

    for (int i = 0; i < 40; i++) {
        if (sum.a > 0.99 || 
            pos.y < (MIN_HEIGHT-1.0) || 
            pos.y > (MAX_HEIGHT+1.0)) break;
        
        float den = density(pos);
        
        if (den > 0.01) {
            float dif = clamp((den - density(pos + 0.3 * sundir))/0.6, 0.0, 1.0);

            vec3 lin = vec3(0.65, 0.7, 0.75) * 1.5 + vec3(1.0, 0.6, 0.3) * dif;        
            vec4 col = vec4( mix( vec3(1.0, 0.95, 0.8) * 1.1, vec3(0.35, 0.4, 0.45), den), den);
            col.rgb *= lin;

            // front to back blending    
            col.a *= 0.5;
            col.rgb *= col.a;

            sum = sum + col*(1.0 - sum.a); 
        }
        
        t += max(0.05, 0.02 * t);
        pos = ro + rd * t;
    }
    
    sum = clamp(sum, 0.0, 1.0);
    
    float h = rd.y;
    sum.rgb = mix(sum.rgb, backCol * 0.75, exp(-20.*mat_cloud_exp * h * h));

    return mix(backCol, sum.xyz, sum.a);
}

float planeIntersect( vec3 ro, vec3 rd, float plane)
{
    float h = plane - ro.y;
    return h/rd.y;
}

/////////
// FOG

vec4 applyFog( in vec3  rgb,      // original color of the pixel
               in float distance, // camera to point distance
               in vec3  rayOri,   // camera position
               in vec3  rayDir,
			   in vec3 pos )  // camera to point vector
{
	float c = 0.1;
	float b = 0.8; // falloff
    float fogAmount = c * exp(-rayOri.y * b) * (1.0 - exp(-distance * rayDir.y * b ))/rayDir.y;
    vec3  fogColor  = horizonColor * 0.8;

    return vec4(mix(rgb, fogColor, fogAmount ),fogAmount);
}


vec4 materialColorForPixel( vec2 texCoord )
{	
	vec2 uv = texCoord;
	uv.y = 1.-uv.y;  // flip for thumb
	uv = uv * 2.-1.; 
	uv.y += 0.25; 

	uv *= mat_scale;

 	vec3 ro=vec3(0, -mat_offset.y * 2., 10.0);
 	vec3 ta=vec3(0);
  
  	ro.xz *= rot(mat_offset.x * PI + mat_animation_time * float(mat_do_anim));

 	float adv = 0.0;
 	ro.z += adv;
 	ro.z += adv;

  	vec3 cz = normalize(ta-ro);
  	vec3 cx = normalize(cross(cz, vec3(vec2(0, -1.)*rot(mat_roll*PI*0.5),0)));
  	vec3 cy = normalize(cross(cz, cx));
  	float fov = mat_fov;
  	vec3 rd=normalize(cx*uv.x + cy*uv.y + fov*cz);
	
	vec3 col = vec3(0.);
    vec2 tmat = intersect(ro,rd);

    // geometry
    vec3 pos = ro + tmat.x*rd;
    vec3 nor = calcNormal(pos);
    vec3 ref = reflect(rd,nor);
	float ao = calcAO(pos,nor);

	float fresnel = pow(clamp(1.1+dot(nor,rd),0.0,0.9),6.0)*0.8;

    // color
    vec4 mate = vec4(0.0);
    if( tmat.y<1 ){
	///////////////
	// clouds
   	float dist = planeIntersect(ro, rd, MIN_HEIGHT);
    
	col = horizonColor;
    if (dist > 0.0) {
        col = raymarching(ro, rd, dist, col);
		tmat.x = dist;
    }

	}

    else if(tmat.y == 2){	 // plane water

		col = waterColor(pos);

		if(mat_do_rfl == true){
			vec2 r_pos = intersect(pos+ref*0.001,ref);
			vec3 new_pos = pos + r_pos.x*ref;
			vec3 new_col = vec3(0.);

			if(r_pos.y >= 3){
				new_col = mountColor(new_pos, nor, rd)*2.-1.;
			} 
			else if(r_pos.y <2) {
				new_col = horizonColor;
			}

			col = mix(col,new_col.xyz,fresnel*0.85);
			}							
		}
		else if(tmat.y >= 3){	// mountain
			col = mountColor(pos,nor,rd)*1.4;
		}

	if(tmat.y >= 2){
		col *= vec3(1.2,1.,1.4);
	}

	col *= vec3(ao);

	vec3 sepia = vec3(207,210,193)/255.;
	vec3 bluish = vec3(80,125,122)/255.;
	float g = smoothstep(0.,1.,texCoord.y);
	vec3 grad = mix(sepia,bluish,vec3(g));
	
	col = mix(col, col*grad*1.5, texCoord.y);

	vec4 fogCol =  applyFog( col,  tmat.x, ro,  rd , pos);
	col = fogCol.rgb;
  	vec3 sky = mix(max(vec3(0),vec3(0.5,.6,1.0)+rd.y*2.0), vec3(0.9,.7,0.4)*3.0, pow(max(0.0,dot(rd,-sun)),10.0));

	col -= pow(fogCol.a, 2.0)*0.1;
 	col += pow(fogCol.a, 2.0)*sky*0.2;

 	col = smoothstep(0.0,1.0,col);
 	col = pow(col, vec3(0.8545));

	/// GRAIN
	return vec4(col,1.0);
}