/*{
    "CREDIT": "Ivan Verdugo",
    "DESCRIPTION": "Chakana Balloon, Andean Cross",
    "TAGS": "chakana, cross, andean, chile, peru, bolivia, andes",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": -3.0, "MAX": 3.0, "DEFAULT": 0.3 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.01, "MAX": 1.0, "DEFAULT": 0.5 }, 
        { "LABEL": "Hue", "NAME": "mat_hue", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },
	    { "LABEL": "Saturation", "NAME": "mat_sat", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
	    { "LABEL": "Brightness", "NAME": "mat_bright", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },				
        { "LABEL": "Inflate", "NAME": "mat_detail", "TYPE": "float", "MIN": 0.001, "MAX": 4.0, "DEFAULT": 0.1 },				

		{ "LABEL": "Auto Hue", "NAME": "mat_auto_hue", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
		{ "LABEL": "Auto Hue Speed", "NAME": "auto_hue_speed", "TYPE": "float", "MIN": 0.0, "MAX": 3.0, "DEFAULT": 0.3 } 
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],
	  "IMPORTED": [
        {"NAME": "mat_some_texture", "PATH": "flare.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"	
	
float det = mat_detail;

// distancia máxima que recorrerá el rayo, lo que esté más allá de
// esta distancia se considera fuera de la escena

float maxdist = 500.;

// máxima cantidad de pasos que dará el raymarching
// un valor estandar puede ser 100
// pero puede que necesitemos más según la escena

int maxsteps = 50;
vec3 objcol;
// FUNCIONES DE DISTANCIA PRIMITIVAS 
// son las que nos devuelven la distancia estimada para diferentes formas geométricas
// en este caso usaremos una de las más simples, una esfera, que es simplemente un length
// de la posición, restándole el radio de la misma

mat2 rot(float a) {
    float s=sin(a), c=cos(a);
    return mat2(c,s,-s,c);
}


vec3 hsb2rgb( in vec3 c ){
    vec3 rgb = clamp(abs(mod(c.x*6.0+vec3(0.0,4.0,2.0),
                             6.0)-3.0)-1.0,
                     0.0,
                     1.0 );
    rgb = rgb*rgb*(3.0-2.0*rgb);
    return c.z * mix(vec3(1.0), rgb, c.y);
}


float sphere(vec3 p, float rad) 
{
    return length(p) - rad;
}

float box(vec3 p, vec3 c)
{
    p=abs(p)-c;
    return length(max(vec3(0.), p) + min(0, max(p.z, max(p.x, p.y))));
}

// FUNCION DE ESTIMACION DE DISTANCIA
// se va a encargar de devolvernos la distancia estimada a uno o varios objetos
// se pueden hacer aquí varias alteraciones a las formas primitivas
// algo que se verá en los próximos ejemplos



float chakana1(vec3 p){
    p.xz *= rot(TIME*mat_speed);
//    p.x -= 1.;
//    p.xy *= rot(time);
//    p.y += 2.;
    
    float d = box(p, vec3(2.5,2.5,1.5));
    
   // p.zy *= rot(time);
    //p.x -= 1.;
    //p.z-=1.;
   // p.xz *= rot(time);
    p.y += 3.;
    float e = box(p, vec3(1.0,1.0,1.5));
    p.y -= 6.;

    float f = box(p, vec3(1.0,1.0,1.5));
    p.y += 3.;
    p.x += 3;
    float g= box(p, vec3(1.0,1.0,1.5));
    p.x -= 6.;
    float h= box(p, vec3(1.0,1.0,1.5));
    
   
    float obj1 = min(e,f);
    float obj2 = min(g,h);
    float adornos = min(obj1,obj2);
    float fin = min(d,adornos);
    return fin;

}


float chakana2(vec3 p){
    p.xz *= rot(TIME);
//    p.x -= 1.;
//    p.xy *= rot(time);
//    p.y += 2.;
    
    float d = box(p, vec3(2.0,2.0,2.0));
    p.y += 3.;
    float e = box(p, vec3(0.5));
    p.y -= 6.;
    float f = box(p, vec3(0.5));
    p.y += 3.;
    p.x += 3;
    float g= box(p, vec3(0.5));
    p.x -= 6.;
    float h= box(p, vec3(0.5));
    p.x+=3.0;
    p.z+=3.0;
    float i= box(p, vec3(0.5));
    p.z-=6.0;
    float j= box(p, vec3(0.5));
    float k= box(p, vec3(0.5));
    float l= box(p, vec3(0.5));
    float obj1 = min(e,f);
    float obj2 = min(g,h);
    float obj3 = min(i,j);
    float obj4 = min(k,l);
    float grupo1 = min(obj1,obj2);
    float grupo2 = min(obj3, obj4);
    
    
    float adornos = min(grupo1,grupo2);
    float fin = min(d,adornos);
    
    
    
    
    
    
    return fin;

}


float de(vec3 p) 
{
    
  
    float fin = chakana1(p);
    
    
    
    
    return fin; 
}


// FUNCION NORMAL
// La normal es el vector que es perpendicular a la superficie para un punto determinado
// nos sirve principalmente para calcular la iluminación en dicho punto
// la fórmula utiliza para este cálculo la diferencia entre la distancia obtenida 
// para 3 puntos apenas desplazados en los ejes x, y, z, 
// con la distancia obtenida en el punto actual.
// (esta explicación es sólo para saber lo que hace, en la práctica la podemos copiar y pegar o
// memorizarla, ya que no hay mucho para experimentar acá)

vec3 normal(vec3 p) 
{   
    vec2 d = vec2(0., det); // det es la distancia que establecimos como el nivel de detalle
    
    // usamos aquí la variable d para establecer un corrimiento de la posición original en x, y, z
    // según si ponemos x o y (de la variable d) en cada posición luego del punto, 
    // estamos estableciendo en que eje va el desplazamiento, que es igual a det
    
    return normalize(vec3(de(p + d.yxx), de(p + d.xyx), de(p + d.xxy)) - de(p));
    
    // también podríamos colocar de(p + vec3(det, 0., 0.)) etc, esto es sólo para hacerlo más fácil
}

// FUNCION SHADE
// Es la que va a establecer el color final según la iluminación, el color de la superficie, etc.

vec3 shade(vec3 p, vec3 dir) {
    
    // establecemos la dirección desde donde viene la luz
    // en este caso es una sola fuente, y no es una luz ubicada en el espacio,
    // sino sólo una dirección desde donde viene, como si fuera una fuente de luz lejana
    // por ejemplo el sol
    
    // x = derecha/izquierda - y = arriba/abajo - z = delante/detrás
    vec3 lightdir = normalize(vec3(1.0, 1.0, -1.5)); 
    
    // como sólo hay un objeto en la escena, definimos su color aquí
    
    //vec3 col = vec3(0., 0 , 1.);
	vec3 col;
	if(mat_auto_hue==true){
      col = hsb2rgb(vec3(0.5+0.3*sin(TIME*auto_hue_speed), mat_sat , mat_bright));
	}  
    if(mat_auto_hue==false){
	  col = hsb2rgb(vec3(mat_hue, mat_sat , mat_bright));
	}
    // obtenemos la normal
    
    vec3 n = normal(p);
    

    
    // calculamos la luz difusa, que es la que se dispersa luego de rebotar en la superficie
    // para esto usamos la función dot (producto escalar), que en este caso la podemos ver
    // como una función que dados dos vectores normalizados (de largo 1.), nos devuelve un valor
    // entre -1. y 1. según cuán alineados están dichos vectores.
    // de esta manera obtenemos el sombreado de la superficie según hacia qué dirección apunta 
    // la misma y la diferencia con la dirección desde donde viene la luz. 
    // usamos la función max porque queremos descartar los valores negativos,
    // si no está apuntando a la luz, que sea 0.
    
    float diff = max(0., dot(lightdir, n));
    
    // calculamos la luz especular, que es la que se refleja directamente como si fuera un espejo,
    // y nos dá lo que podríamos llamar como el "brillo" en la superficie.
    
    // primero obtenemos, con la función reflect que ya trae GLSL, el vector reflejo entre
    // la dirección en la que va el rayo y la superficie
    
    vec3 refl = reflect(dir, n);
    
    // caculamos la luz especular, usando también la función dot obtenemos 
    // la diferencia entre este vector y la dirección desde que viene la luz, 
    // elevada a una potencia con la función pow, que nos va a determinar el tamaño del "brillo"
    
    float spec = pow(max(0., dot(lightdir, refl)), 500);
    
    // la luz ambiental es la que va a iluminar uniformemente toda la superficie
    float amb = .1;

    // este es una de las formas de calcular la combinación de las luces
    // considerando una luz blanca que golpea un objeto uniformemente azul
    // y es el color multiplicado por la suma de la luz ambiental y la difusa,
    // sumandole a ese resultado la luz especular (brillo)
    
    // podemos alterar los valores de estas variables para modificar la iluminación
    // en este caso le bajé un poco el brillo a la especular
    
    return col*(amb+diff) + spec * 99.9*diff;
    
}

vec3 march(vec3 from, vec3 dir) 
{
    // variables que vamos a usar
    // d = distancia actual al objeto más próximo
    // td = distancia total recorrida desde la cámara
    // p = posición actual del rayo
    // col = color final

    float d, td=0.;
    vec3 p, col;

    // bucle del raymarching
    // a cada paso avanzará según la distancia obtenida 
    // en la posición actual, que nos dará la función de distancia de()

    for (int i=0; i<maxsteps; i++) 
    {
        // obtenemos la posición actual de rayo para esta iteración
        // distancia de la cámara + total distancia recorrida * dirección hacia la que va el rayo
        // en el primer paso td = 0 por lo que el rayo está en la posición de la cámara

        p = from + td * dir;

        // llamamos a la función de estimación de distancia, que devolverá
        // la distancia desde este punto al objeto más cercano

        d = de(p);

        // si la distancia es menor al umbral que definimos para determinar si se chocó con un objeto,
        // o bien el rayo sobrepasó la distancia máxima que especificamos, cortamos el for

        if (d < det || td > maxdist) break;

        // sumamos la nueva distancia obtenida en el acumulador, el rayo avanza

        td += d;
    }

    // Una vez que el for termina, se decide qué hacer según si el rayo golpeó una superficie o no

    if (d < det) // el rayo chocó con una superficie
    {
        // retrocedemos el rayo un paso atrás con distancia igual a det
        // esto es para asegurarnos que estamos "fuera" de la distancia establecida
        // por det como la mínima para determinar que se chocó con un objeto
        // y mejora el cálculo de la normal, evitando banding y artefactos
        
        p -= dir * det; 
        
        // llamamos a la función shade, que se encargará de sombrear la superficie
        // para este punto según la iluminación que reciba

        col = shade(p, dir);
    }
    else // el rayo no chocó con una superficie
    {
        // aquí podemos dibujar un fondo por ejemplo
        col = vec3(0.0);
        //col = mod(gl_FragCoord.y,1.)*vec3(.6,0.0,0.);
        //col = mod(gl_FragCoord.y,0.5+0.5*sin(time*0.0001))*smoothstep(vec3(0.5+0.5*sin(time*0.3),1.0,0.0),vec3(1.0,0.0,0.0),vec3(0.5+0.5*cos(time*0.1),0.5+0.5*cos(time*0.2),0.5+0.5*cos(time*0.1))) ;
    }
    
    
    return col;    
}	
	

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
   uv-=0.5;
	uv.y *= RENDERSIZE.x/RENDERSIZE.y;
	uv.x *= RENDERSIZE.x/RENDERSIZE.y;

	
	uv*=mat_scale;
	
	vec3 from = vec3(0.0,0.0,-15.0);
	vec3 dir = normalize(vec3(uv,1.0));
	vec3 col = march(from,dir);
	
    
		
	vec4 final_color = vec4(col,1.0);	
	return final_color;
}
