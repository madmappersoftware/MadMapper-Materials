/*{
    "CREDIT": "voithos",
    "DESCRIPTION": "Triangular Noise, useful to raymarch a triangulated terrain. Noise Value in R, distance to edge in G",
    "TAGS": "noise",
    "VSN": "1.0",
    "INPUTS": [ 
		{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
{"LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 40.0, "DEFAULT": 20.0 }, 
{"LABEL": "Noise+Edge", "NAME": "mat_edge", "TYPE": "bool",  "DEFAULT": false, "FLAGS":"button" }, 
    ],
	"GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],
}*/

/// https://www.shadertoy.com/view/DtXXRM
// by voithos

float pow1d5(float a) { return a * sqrt(a); }

float hash21(vec2 co) {
  return fract(sin(dot(co.xy, vec2(1.9898, 7.233))) * 45758.5433);
}

float hash(vec2 uv) {

	return pow1d5(hash21(uv));
}

float edgeMin(float dx, vec2 da, vec2 db) {
  return min(min((1. - dx) * db.y, da.x), da.y);
}

// 2D triangular noise, red channel denotes height, green is distance to nearest
// edge.
vec2 trinoise(vec2 uv) {
  const float sq = sqrt(3. / 2.);
  uv.x *= sq;
  uv.y -= .5 * uv.x;
  vec2 d = fract(uv);
  uv -= d;

  bool c = dot(d, vec2(1)) > 1.;

  vec2 dd = 1. - d;
  vec2 da = c ? dd : d, db = c ? d : dd;

  float nn = hash(uv + float(c));
  float n2 = hash(uv + vec2(1, 0));
  float n3 = hash(uv + vec2(0, 1));

  float nmid = mix(n2, n3, d.y);
  float ns = mix(nn, c ? n2 : n3, da.y);
  float dx = da.x / db.y;
  return vec2(mix(ns, nmid, dx), edgeMin(dx, da, db));
}

vec4 materialColorForPixel( vec2 texCoord )
{
	vec2 uv = texCoord *2.-1.;
	uv *= mat_scale;

	// generate Noise Value and distance to Edge
	vec2 n = trinoise(uv);

	if(mat_edge) return vec4(n,0.,1.);

	return vec4(n.x,n.x,n.x,1.);
}