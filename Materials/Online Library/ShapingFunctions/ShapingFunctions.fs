/*{
    "CREDIT": "Matimoo",
    "DESCRIPTION": "https://www.shadertoy.com/view/M3VyDz",
    "TAGS": "template",
    "VSN": "1.0",
    "INPUTS": [ 
		{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
    ],
	"GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],
}*/

#define PI 3.14159265359

// Almost Identity / Smooth Clamp
// m : smooth threshold
// n : clamp value
float smooth_clamp(float x, float m, float n) {
    if(x>m) return x;

    // commence à (0,n) et passe au travers du point (m,m)
    // la dérivée au point (0,n) == 0 et au point (m,m) == 1
    // prenons le polynome de degrée 3 : f(x)=ax³+bx²+cx+d
    // f(0)=d=n
    // f(m)=am³+bm²+cm+n=m
    // f'(m)=3am²+2bm+c=1
    // f'(0)=c=0
    // 2 inconnus (a et b), 2 équations --> résolution possible
    // on obtiens : f(x)=((2n-m)/m³)x³+((2m-3n)/m²)x²+n
    // la forme si dessous est une simplification
    float a = 2.0*n-m;
    float b = 2.0*m-3.0*n;
    float t = x/m;
    return (a*t+b)*t*t+n;

    // apprentissages : il est utile de définir les contraintes de notre fonction
    // afin de trouver la fonction désirée
}

// Exponential Impulse
// s : strength
// h : height
float exp_impulse(float x, float s, float h) {
    float k = s*x;
    float j = h*k;
    // 1-k inverse la direction de l'exponentielle (de haut vers le bas)
    return j*exp(1.0-k);

    // note : la fonction si dessus multiplie l'exponentielle par x ("j")
    // je ne comprends pas encore pourquoi, mais multiplier par x peut donner des résultats intéressants?
    // comme faire des bosses? à voir...
}

// Acceleration to constant speed
// a : acceleration
// t : target speed
float acc_to_const_v(float x, float a, float t) {
    // formule de la position dans le temps ax²+vx+p 
    // pas de vitesse et position initiale donc p(x)=ax²
    // en faisant la dérivée de la formule, on obtient la formule de la vitesse : v(x)=2ax
    if (2.0*a*x < t) return a*pow(x,2.0);

    // lorsque la vitesse atteint un certain seuil, les valeurs initiales de notre formule changent
    // : a=0, v=t, p : v(x)=2ax -> t=2ax -> x=t/2a -> p(x)=ax² -> p=a(t/2a)²
    return t*x-(a*pow(t/(2.0*a),2.0));

    // apprentissages : pour trouver la droite tangente d'une fonction nommée f(x) à un certain x nommé t
    // y=ax-b : a=f'(t), b=at-f(t) <- isole b dans la droite tangente lorsque x=t et y=f(t)
}

// Sinus Bounce Impulse
// f : frequency
// h : max height
float sin_bounce(float x, float f, float h) {
    float k = PI*(f*x-1.0);
    return h*sin(k)/k;
}

// Light Falloff (to infinity)
// r : distance from light
float light_falloff(float r, float a, float b) {
    r = abs(r);
    return 1.0/(1.0+a*r+b*pow(r,2.0));

    // apprentissages : faire |x| dans une fonction y=...x force la symétrie centrée à 0
}

// Light Falloff (to m)
// r : distance from light
// m : trunc distance
float trunc_falloff(float r, float m) {
    if (r < 0.0) return 1.0;
    if (r > m) return 0.0;
    r /= m;
    return (r-2.0)*r+1.0;
}

// Parabola Wave
// k : strength of wave
float p_wave(float x, float k) {
    return -pow(abs(2.0*x-1.0),1.0/k)+1.0;

    // apprentissages : utilisation d'abs() pour forcer une symétrie
    // et pow(..., k) semble affecter la courbature de la fonction
    // par exemple : fonctionne sur sin() ou cos()
    // k>1 : compression/aplatissement vers 0 et étirement des extremums
    // 0<k<1 : aplatissement des extremums et étirement des valeurs proche de 0
}

// Parabola Bell
// k : strength of bell shape
float p_bell(float x, float k) {
    return pow(4.0*x*(1.0-x),k);

    // apprentissages : une parabole peut être représentée sous trois formes
    // standard : ax²+bx+c où le signe de a contrôle la direction de l'ouverture de la parabole
    // sommet : a(x-h)²+k où le sommet de la parabole = (h,k)
    // intercepte : a(x-p)(x-q) où le sommet de la parabole = ((p+q)/2,f((p+q)/2))
    // dans la forme intercepte p et q sont les deux x par lesquels passent la parabole
}

// Parabola Generic
// a : left side strength of bell shape
// b : right side strength of bell shape
float p_curve(float x, float a, float b) {
    // calculer k en fonction de nos exposants nous permet effectivement de garder la hauteur de notre sommet à 1
    // on trouve le x du sommet grâce à la dérivée et à ses zéros : x=0, x=a/(b+a)
    // en sachant que y=1 et que x=a/(b+a), on peut calculer k
    float k = pow(a+b,a+b)/(pow(a,a)*pow(b,b));

    // puisque nous sommes sous la forme intercepte de la parabole
    // ajouter un exposant à chaque x, affecte la courbature de chaque côté de la parabole séparément
    return k*pow(x,a)*pow(1.0-x,b);
}

// Contrast Control
// k : strength
float flatten(float x, float k) {
    float a = 0.5*pow(abs(2.0*x-1.0),k)+0.5;
    return x>0.5?a:1.0-a;
}

// Contrast Control
float contrast(float x, float k) {
    float a = 0.5*pow(2.0*((x<0.5)?x:1.0-x),k);
    return (x<0.5)?a:1.0-a;
}

// Tonemap Control / Midtones Control
// k : strength
float tonemap(float x, float k) {
    return x*(1.0+k)/(1.0+k*x);

    // notes : fonction inverse : 1/x
}

// apprentissages : en imagerie / color grading, il existe trois opérations primaires pour ajuster une image
// l'offset, qui ajuste les noirs : color + offset
// le gain, qui ajuste les blancs : color * gain
// et le gamma, qui ajuste les tons moyens : color ^ 1/gamma
// très bonne explication des courbes de gamma et pourquoi il est important d'enregistrer les couleurs de manière non linéaire : https://www.youtube.com/watch?v=wFx0d9c8WMs

// Gaussian
// a : height
// b : position
// c : width
float gaussian(float x, float a, float b, float c) {
    return a*exp(-pow(x-b,2.0)/2.0*c*c);
}

// Cubic Pulse / Clamped Gaussian
// c : position
// w : width
float cubic_pulse(float x, float c, float w) {
    x = abs(x - c);
    if (x > w) return 0.0;
    x /= w;
    return 1.0-x*x*(3.0-2.0*x);
}

// Plot Y with a smoothstepped line
float plot(vec2 st, float pct) {
    return smoothstep(pct-0.02, pct, st.y) - smoothstep(pct, pct+0.02, st.y);
}

// Plot Y with cubic_pulse function instead of two smoothstep functions
float plot_v2(vec2 st, float pct) {
    return cubic_pulse(st.y, pct, 0.02);
}

float shaping_func(ivec2 cell_coord, float x) {
    return    cell_coord==ivec2(0,0) ? smooth_clamp(x, 0.95, 0.50)
            : cell_coord==ivec2(1,0) ? exp_impulse(x, 8.0, 0.2)
            : cell_coord==ivec2(2,0) ? acc_to_const_v(x, 4.0, 2.0)
            : cell_coord==ivec2(3,0) ? sin_bounce(x, 8.0, 1.)
            : cell_coord==ivec2(0,1) ? light_falloff(x, 2.0, 5.0)
            : cell_coord==ivec2(1,1) ? trunc_falloff(x, 0.5)
            : cell_coord==ivec2(2,1) ? p_wave(x, 5.0)
            : cell_coord==ivec2(3,1) ? p_bell(x, 5.0)
            : cell_coord==ivec2(0,2) ? p_curve(x, 0.7, 1.5)
            : cell_coord==ivec2(1,2) ? flatten(x, 3.0)
            : cell_coord==ivec2(2,2) ? contrast(x, 3.0)
            : cell_coord==ivec2(3,2) ? tonemap(x, 10.0)
            : cell_coord==ivec2(0,3) ? gaussian(x, 1.0, 0.5, 10.0)
            : cell_coord==ivec2(1,3) ? cubic_pulse(x, 0.5, 1.0)
            : cell_coord==ivec2(2,3) ? 0.0
            : 1.0;
}

vec4 materialColorForPixel( vec2 texCoord )
{
    float column = 4.0;
    float row = 4.0;
    
    vec2 st = texCoord;
	st.y = 1.-st.y;
    st.x *= column;
    st.y *= row;
    ivec2 coord = ivec2(st);
    st = mod(st, 1.0); // mod(..., 1.0) -> garde uniquement la partie décimale

    float y = shaping_func(coord, st.x);
    float pct = plot_v2(st, y);

    vec3 bg_color = vec3(y);
    vec3 plot_color = vec3(1.0,float(coord.y)/(row-1.0),float(coord.x)/(column-1.0));
    vec3 color = (1.0-pct)*bg_color + pct*plot_color;

    float circle_radius = 0.05;
    float cell_num = mod(mat_time, column*row); // pour savoir la cellule dans la grille 4x4 (0...15) selon le temps
    float cell_x = mod(cell_num, column); // pour savoir la position x de la cellule (0...3)
    float cell_y = (cell_num-cell_x)/row; // pour savoir la position y de la cellule (0...3) ex : x=2 -> 14-2 = 12 -> 12/4 = 3 -> y=3
    ivec2 circle_coord = ivec2(int(cell_x), int(cell_y));
    float time_x = mod(mat_time, 1.0);

    float circle_y = shaping_func(coord, time_x);
    float circle_pct = float(length(vec2(time_x, circle_y)-st) <= circle_radius);

    vec3 circle_color = vec3(1.0-circle_y);
    color = (1.0-circle_pct)*color + circle_pct*circle_color;

    return vec4(color, 1.0);

}