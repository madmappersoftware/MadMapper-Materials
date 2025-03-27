/*{
    "RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "Adapted by Cornelius // ProjectileObjects // Based on VDMX VIDVOX NYE ISF Shader and tutorial by David Lublin (https://vdmx.vidvox.net/tutorials/how-to-do-a-new-years-eve-countdown)",
    "DESCRIPTION": "A laser shader that displays a clock (Time-of-day, Countdown) or Counter using DATE / TIME, with 7-segment lines. The Counter starts at 00:00:00.",
    "TAGS": "time, clock, laser, segment",
    "VSN": "1.3",
    "INPUTS": [
        {
            "NAME" : "colorInput",
            "TYPE" : "color",
            "DEFAULT" : [1.0, 0.5899133, 0.0, 1.0],
            "LABEL" : "Color"
        },
        {
            "VALUES" : [0,1,2],
            "NAME" : "clockMode",
            "TYPE" : "long",
            "DEFAULT" : 0,
            "LABEL" : "Clock Mode",
            "LABELS" : ["Time","Countdown","Application Counter"]
        },
        {
            "NAME" : "yOffset",
            "TYPE" : "float",
            "MAX" : 1,
            "DEFAULT" : 0,
            "MIN" : -1,
            "LABEL" : "Y Offset"
        },
        {
            "NAME" : "blinkingColons",
            "TYPE" : "bool",
            "DEFAULT" : true,
            "LABEL" : "Blink"
        },
        {
            "NAME" : "twentyFourHourStyle",
            "TYPE" : "bool",
            "DEFAULT" : false,
            "LABEL" : "24 Hour"
        },
        {
            "NAME" : "design_scale",
            "TYPE" : "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 0.170,
            "LABEL": "Scale"
        },
        {
            "NAME": "global_x",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0,
            "LABEL": "Position X"
        },
        {
            "NAME": "global_y",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0,
            "LABEL": "Position Y"
        }
    ]
}*/

////////////////////////////////////////////////////////////
// In MadMapper & other ISF hosts, "DATE" (vec4) and "TIME" (float)
// are automatically provided (no need to declare or 'extern').
//
// clockMode => 0=Time-of-day (DATE.a), 1=Countdown (86400 - DATE.a), 2=Counter (floor(TIME) => starts at 00:00:00).
////////////////////////////////////////////////////////////

const float PI = 3.14159265359;

// Each of the 7 segments is a line from startPt to endPt
struct SegmentLine {
    vec2 startPt;
    vec2 endPt;
};

// The 7 segments for one digit
SegmentLine[7] segments = SegmentLine[](
    // A (top)
    SegmentLine(vec2(-0.3,  0.5), vec2( 0.3,  0.5)),
    // B (upper right)
    SegmentLine(vec2( 0.3,  0.5), vec2( 0.3,  0.0)),
    // C (lower right)
    SegmentLine(vec2( 0.3,  0.0), vec2( 0.3, -0.5)),
    // D (bottom)
    SegmentLine(vec2(-0.3, -0.5), vec2( 0.3, -0.5)),
    // E (lower left)
    SegmentLine(vec2(-0.3,  0.0), vec2(-0.3, -0.5)),
    // F (upper left)
    SegmentLine(vec2(-0.3,  0.5), vec2(-0.3,  0.0)),
    // G (middle)
    SegmentLine(vec2(-0.3,  0.0), vec2( 0.3,  0.0))
);

////////////////////////////////////////////////////////////
// 7-segment "on/off" map for digits 0..9
////////////////////////////////////////////////////////////
bool[7] DIGIT_MAP(int d) {
    bool[7] blank = bool[7](false,false,false,false,false,false,false);
    bool[7] zero  = bool[7](true, true,  true,  true,  true,  true,  false);
    bool[7] one   = bool[7](false,true,  true,  false, false, false, false);
    bool[7] two   = bool[7](true, true,  false, true,  true,  false, true);
    bool[7] three =bool[7](true, true,  true,  true,  false, false, true);
    bool[7] four  =bool[7](false,true,  true,  false, false, true,  true);
    bool[7] five  =bool[7](true, false, true,  true,  false, true,  true);
    bool[7] six   =bool[7](true, false, true,  true,  true,  true,  true);
    bool[7] seven =bool[7](true, true,  true,  false, false, false, false);
    bool[7] eight =bool[7](true, true,  true,  true,  true,  true,  true);
    bool[7] nine  =bool[7](true, true,  true,  true,  false, true,  true);

    if(d<0||d>9) return blank;
    if(d==0)return zero; if(d==1)return one; if(d==2)return two; if(d==3)return three; if(d==4)return four;
    if(d==5)return five;if(d==6)return six; if(d==7)return seven;if(d==8)return eight;if(d==9)return nine;
    return blank;
}

////////////////////////////////////////////////////////////
// Main laserMaterialFunc
////////////////////////////////////////////////////////////
void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
    // The ISF host provides 'DATE' (vec4) and 'TIME' (float) automatically.
    // We do not declare them as uniform or extern to avoid errors.

    // 1) Determine baseSec from clockMode
    float daySecs = DATE.a; // system time-of-day in seconds
    float runSecs = TIME;   // free-run since shader loaded
    float baseSec = 0.0;

    if(clockMode==2) {
        // mode=2 => Counter => floor(TIME) to ensure it starts at 00:00:00 
        // right away, ignoring fractional leftover from load time
        baseSec = floor(runSecs);
    }
    else if(clockMode==1) {
        // countdown => 86400 - daySecs
        baseSec = 86400.0 - daySecs;
        if(baseSec<0.0) baseSec=0.0;
    }
    else {
        // mode=0 => normal time => mod 24h
        baseSec = mod(daySecs,86400.0);
    }

    // parse out h,m,s
    float tmpVal=floor(baseSec);
    float s = mod(tmpVal,60.0);  tmpVal=floor(tmpVal/60.0);
    float m = mod(tmpVal,60.0);  tmpVal=floor(tmpVal/60.0);
    float h = mod(tmpVal,60.0);

    // 12-hour logic if !twentyFourHourStyle && mode=0
    if(!twentyFourHourStyle && clockMode==0){
        float hh=mod(h,12.0);
        if(hh<1.0) hh=12.0;
        h=hh;
    }

    // digits => [H tens, H ones, M tens, M ones, S tens, S ones]
    int Ht=int(floor(h/10.0));
    int Ho=int(mod(h,10.0));
    int Mt=int(floor(m/10.0));
    int Mo=int(mod(m,10.0));
    int St=int(floor(s/10.0));
    int So=int(mod(s,10.0));
    int digits[6]=int[](Ht,Ho, Mt,Mo, St,So);

    // total lines => 42 for digits +4 for colons =>46 => totalPoints=92
    int digitSegCount=6*7;  //42
    int colonLineCount=4;   //2 colons =>2 lines each
    int lineCount=digitSegCount+colonLineCount; //46
    int totalPoints=lineCount*2; //92

    if(pointNumber>=totalPoints){
        pos=vec2(0); color=vec4(0); shapeNumber=0; 
        return;
    }

    int lineIndex=pointNumber/2;
    bool isStart=(pointNumber%2==0);

    // blinking colons => if fract(baseSec)>=0.5 => off
    bool showColons=true;
    float fracSec=fract(baseSec);
    if(blinkingColons) {
        if(fracSec>=0.5) showColons=false;
    }

    // digits: 0..41 lines, colons: 42..45 => total 46 lines
    if(lineIndex<digitSegCount){
        // digit line
        int dIndex=lineIndex/7;
        int segIndex=lineIndex%7;
        if(dIndex>5){ pos=vec2(0); color=vec4(0); shapeNumber=0; return;}
        bool[7] segMap = DIGIT_MAP(digits[dIndex]);
        if(!segMap[segIndex]) {
            // skip this segment
            pos=vec2(0); color=vec4(0); shapeNumber=0; 
            return;
        }
        // retrieve the line
        vec2 linePos=(isStart? segments[segIndex].startPt : segments[segIndex].endPt);
        // offset each digit by 1.0 in x => total 6 digits => shift them ~2.5 to center
        float xOff=float(dIndex)*1.0 -2.5;
        linePos.x+=xOff;
        // apply scale, yOffset
        float adjOff=yOffset*1.2; 
        linePos*=design_scale;
        linePos.y-= adjOff;
        linePos += vec2(global_x,global_y);

        pos=linePos;
        color=colorInput;
        shapeNumber=lineIndex;
    }
    else {
        // colon lines => lineIndex-digitSegCount =>0..3
        int cIndex=lineIndex-digitSegCount;
        if(cIndex>=4){
            pos=vec2(0); color=vec4(0); shapeNumber=0; return;
        }
        if(!showColons){
            pos=vec2(0); color=vec4(0); shapeNumber=0; return;
        }

        // colon0 => between digit1&2 => x= -1.0 offset
        // colon1 => between digit3&4 => x= +1.0 offset
        int colonID = cIndex/2; // 0 or1
        bool top = ((cIndex%2)==0);
        float xC=(colonID==0)? -1.0 : +1.0;
        float yC= top? 0.2 : -0.2;
        vec2 cPos=vec2(xC,yC)*design_scale;
        cPos.y-=(yOffset*1.2);
        cPos+=vec2(global_x,global_y);

        pos=cPos;
        color=colorInput;
        shapeNumber=lineIndex;
    }
}