/*{
    "CREDIT": "1024 architecture\nFranz",
    "DESCRIPTION": "Inpired by Tame Impala - Currents / 2015",
    "TAGS": "album cover",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Quality", "NAME": "mat_quality", "TYPE": "int", "MIN": 10, "MAX": 100, "DEFAULT": 50 },
		{ "LABEL": "Enable Reflection", "NAME": "mat_do_rfl", "TYPE": "bool",  "DEFAULT": 0,"FLAGS":"button" }, 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0 }, 
		{ "LABEL": "Speed reflect", "NAME": "mat_speed2", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 2. }, 
		{ "LABEL": "Power reflect", "NAME": "mat_pow", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 0.56 },
		{ "LABEL": "Width", "NAME": "mat_width", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.176 },
		{ "LABEL": "Sphere Radius", "NAME": "mat_radius", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.296 }, 
		{ "LABEL": "Sphere Pos", "NAME": "mat_pos", "TYPE": "float", "MIN": -1.0, "MAX": 2.0, "DEFAULT": 0.0 }, 
		{ "LABEL": "Noise Sphere", "NAME": "mat_noise", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
		{ "LABEL": "Noise Sphere2", "NAME": "mat_noise2", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
		{ "LABEL": "Noise Grid", "NAME": "mat_noise_grid", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Target", "NAME": "mat_target", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 0.3, -0.15 ] },
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ -0.43, 0.42 ] },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_t", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed2", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],
	  "IMPORTED": [
        {"NAME": "mat_rfl", "PATH": "env.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
		{"NAME": "mat_rfl_speed", "PATH": "env_speed.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

#define AA 4    // 1: no AA, 2 or 4: AA

const vec2 res = vec2(1080.);
const float precis = 0.001;

//////// Utility
vec2 rotate2d(vec2 v, float a) {
	float s = sin(a);
	float c = cos(a);
	mat2 m = mat2(c, -s, s, c);
	return m * v;
}

mat4 rotationMatrix(vec3 axis, float angle) {
    axis = normalize(axis);
    float s = sin(angle);
    float c = cos(angle);
    float oc = 1.0 - c;
    
    return mat4(oc * axis.x * axis.x + c,           oc * axis.x * axis.y - axis.z * s,  oc * axis.z * axis.x + axis.y * s,  0.0,
                oc * axis.x * axis.y + axis.z * s,  oc * axis.y * axis.y + c,           oc * axis.y * axis.z - axis.x * s,  0.0,
                oc * axis.z * axis.x - axis.y * s,  oc * axis.y * axis.z + axis.x * s,  oc * axis.z * axis.z + c,           0.0,
                0.0,                                0.0,                                0.0,                                1.0);
}
vec3 rotate3d(vec3 v, vec3 axis, float angle) {
	mat4 m = rotationMatrix(axis, angle);

	return (m * vec4(v, 1.0)).xyz;
}

////// Signed Distance Fields
vec2 sdSphere( vec3 p, float r ){
	float n = (fBm(vec3((p.yz * 0.5), mat_animation_time * 10.)) * 0.5 - 0.5) * mat_noise;
	float n2 = (billowedNoise(vec3(p.yz, mat_animation_time * 10.)) * 0.15) * mat_noise2;

	return vec2(length(p) - r- n - n2, 1.);
}

float genMask(in vec3 pos){
	vec3 p = pos;
	p.y -= 0.5 + mat_pos * 10.;

	vec2 uv_a = rotate2d(p.yz, 0.7);
	vec2 uv_b = rotate2d(p.yz, -0.5);
	float mask_1 = smoothstep(0., 2., -uv_a.x);
	float mask_2 = smoothstep(0., 2., -uv_b.x);

	float mask = mask_1 * mask_2;
	return min(max(0., mask), 0.3);
}

vec2 sdPlane( vec3 p, vec3 no, float h )
{
	float mask = genMask(p);

	float t = mat_animation_time;
	float n = flowNoise(p.yz * min(max(0., -p.y), 1.) * vec2(0.9, 0.5) + vec2(t * 10., t) + vec2(p.z, 0.), t) * 0.25;
	n *= mat_noise_grid * 1.5;

	float hh = n * mask;
	float center_bump = min( max(1. -length(p.yz + vec2(1.-mat_pos * 10., 0)), 0.) * 0.5, 0.5);

  	return vec2(dot(p,no) + hh + center_bump - (n * center_bump) * 5. * mat_radius, 3.);
}

vec2 map( vec3 p )
{	
	vec2 d1 = sdPlane(p, vec3(1., 0., 0.), 0.);
	vec2 d2 = sdSphere(p-vec3(0.1, -1. + mat_pos * 10., 0.), mat_radius * 2.);	

	if(d2.x < d1.x) d1 = d2;

	return d1;
}

vec2 intersect( in vec3 ro, in vec3 rd )
{
	float h = precis * 2.0;
    vec3 c;
    float t = 0.0;
	float maxd = 20.0;
    float sid = -1.0;
    for( int i=0; i < mat_quality; i++ )
    {
        if( abs(h)<precis||t>maxd ) break;
        t += h;
	    vec2 res = map( ro + rd * t );
        h = res.x;
	    sid = res.y;
    }

    if( t>maxd ) sid=-1.0;

    return vec2( t, sid );
}

vec3 calcNormal( in vec3 pos )
{
    vec3  eps = vec3(precis, 0.0, 0.0);
    vec3 nor;

    nor.x = map(pos+eps.xyy).x - map(pos-eps.xyy).x;
    nor.y = map(pos+eps.yxy).x - map(pos-eps.yxy).x;
    nor.z = map(pos+eps.yyx).x - map(pos-eps.yyx).x;

    return normalize(nor);
}

vec4 sphereColor( in vec3 pos, in vec3 nor, in vec3 ref){

	vec3 r = ref;
	r = rotate3d(ref, normalize(vec3(-1, 1., -1)), PI);

	vec2 uv_r = vec2( atan( r.x, r.z ), acos(r.y) );
	uv_r *= 0.125;

	vec3 rfl = texture(mat_rfl, fract(-uv_r + vec2(0.6, 0.1))).xyz;
	rfl = pow(rfl + 0.5, vec3(4.2));

	float rfl_speed = texture(mat_rfl_speed, pos.xz + vec2(mat_t, 0.)).x * 4.;

	return vec4( rfl  + vec3(rfl_speed) * mat_pow + abs(nor.xyz) * 0.25, 1.);
}

vec4 floorColor( in vec3 pos, in vec3 nor )
{
	vec3 p = pos;
	float mask = genMask(pos);

	// circular deformation
	vec2 cen = p.yz-vec2(-0.9 + mat_pos * 10., 0.1); // center of deformation
	float c = 1. -min(length(cen), 1);
	c = pow(c + 0.5, 2.5);
	vec2 dir = normalize(cen) * c;
	p.yz -= dir;

	// extra noise deformation texture
	float n = noise(pos * vec3(1., 3, 3.) * 0.6 + vec3(0., mat_animation_time * 10., 0)) * mask;
	p.yz += n * mat_noise_grid;

	float C_1 = smoothstep(1.-mat_width-0.1, 1.-mat_width, fract(p.z * 10.));
	float C_2 = 1. - smoothstep(1.-mat_width+.1, 1., fract(p.z * 10.));
    vec3 col = vec3(C_2 * C_1);

	float mask_center = 1. - smoothstep(0.05, 0.07, abs(p.z - 0.05));

	vec3 lineCol = mix(vec3(1., 0., 0.7), vec3(1., 0.8, 0.), clamp((-p.y-1.), 0., 1));
	lineCol = mix(lineCol, vec3(1., 0., 0), max(p.y * 0.3, 0.));

	col = mix(col*vec3(0.91, 0.85, 1.), lineCol, mask_center);
	
	return vec4(col, 1.);
}

float calcAO( in vec3 pos, in vec3 nor )
{
	float occ = 0.0;
    float sca = 1.0;
 
    for( int i=0; i < 5; i++ )
    {
        float hr = 0.01 + 0.12 * float(i)/4.0;
        vec3 aopos =  nor * hr + pos;
        float dd = map( aopos ).x;
        occ += -(dd-hr) * sca;
        sca *= 0.95;
    }
    return clamp( 1.0 - 3.0 * occ, 0.0, 1.0 );    
}

vec4 materialColorForPixel( vec2 texCoord )
{	
	vec2 uv = texCoord;
	uv.y = 1.-uv.y;  // flip for thumb
	uv = uv * 2.-1.;  
	uv *= mat_scale;

   	vec2 mo = mat_offset;
	vec2 p = uv;

    // camera
	float an1 = mo.x;
	float an2 = 1. + mo.y;
    vec3 ro = 8. * normalize(vec3(sin(an2) * cos(an1), cos(an2)-0.5, sin(an2) * sin(an1)));
	vec3 ta = vec3(0.0, mat_target);
    vec3 ww = normalize(ta - ro);
    vec3 uu = normalize(cross( vec3(0.0, 1.0, 1.0), ww ));
    vec3 vv = normalize(cross(ww, uu));
    vec3 rd = normalize( p.x * uu + p.y * vv + 1. * ww );
	
	vec3 col = vec3(0.);
    vec2 tmat = intersect(ro,rd);

    // geometry
    vec3 pos = ro + tmat.x * rd;
    vec3 nor = calcNormal(pos);
    vec3 ref = reflect(rd, nor);
	float ao = calcAO(pos ,nor);
	float rim = pow(clamp(1.1 + dot(nor, rd), 0.0, 1.0), 3.0) * 0.7;

    // color
	vec3 tot = vec3(0.);

    #if AA>1
    for( int m=0; m<AA; m++ )
    for( int n=0; n<AA; n++ )
    {
        // pixel coordinates
        vec3 o = vec3(float(m), float(n), 0.) / float(AA) - 0.5;
		o /= res.x * 0.5;
        pos = pos + o;

	#endif

    vec4 mate = vec4(0.0);
    if( tmat.y<1.5 ){
            mate =  sphereColor(pos, nor, normalize(ref + vec3(0.1, -1., 0.)));
			col += 0.2 * max(rim, 0.6);
			col += 1.0 * rim * pow(mate.w, 3.0) * mate.xyz;
			col *= vec3(0.8, 0.9, 1.);
			col += mate.rgb * (1.-rim) * 0.2;
	#if AA ==2
			col *= (1./(AA * 0.75));
	#endif
	#if AA ==4
			col *= (1./(AA * 0.4));
	#endif
			col = clamp(col, vec3(0.), vec3(1.));
			
		// retrace for reflection
		if(mat_do_rfl == true){
			vec2 r_pos = intersect(pos + ref * 0.001, ref);
			vec3 new_pos = pos + r_pos.x * ref;
			vec4 new_col = floorColor(new_pos, nor);

			col += new_col.xyz*rim;	
		}		
	}
    else{
            mate = floorColor(pos, nor);
			col = mate.xyz;		
	}
	tot += col;

    #if AA>1
    }
    tot /= float(AA * AA);
    #endif

	float vignette = 1. - clamp(abs(length(pos)) * 0.04, 0., 0.3);
	vignette  = pow(vignette + 0.1, 8.2);
	tot = mix(tot, tot * clamp(vignette, 0., 1.) * vec3(1., 0.8, 1.), smoothstep(0, 0.5, uv.y-0.1));

	tot *= vec3(ao);

	return vec4(tot, 1.0);
}