/*{
    "CREDIT": "Jason Beyers",

    "DESCRIPTION": "A flexible video wall system for 1-4 input textures, plus a tile overlay and mask.  With animated tile shuffle, tile size, tile opacity, scroll, wobble, scale, and rotation.  With FX for each texture, handy when the textures are images.  Enjoy!",

    "VSN": "1.0",

    "INPUTS": [
        {
            "LABEL": "Texture Mode/Lock Texture Aspect",
            "NAME": "mat_lock_aspect",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "UV/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "UV/Rotate",
            "NAME": "mat_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "UV/Shift",
            "NAME": "mat_shift_amount",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },
        {
            "LABEL": "UV/Shift Scale",
            "NAME": "mat_shift_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "UV/Shift Type",
            "NAME": "mat_shift_type",
            "TYPE": "long",
            "VALUES": ["Pre Rotate","Post Rotate"],
            "DEFAULT": "Pre Rotate"
        },
        {
            "LABEL": "UV/Mirror X",
            "NAME": "mat_mirror_x",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button",
        },
        {
            "LABEL": "UV/Mirror Y",
            "NAME": "mat_mirror_y",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button",
        },

        {
            "LABEL": "Tiles/Mode",
            "NAME": "mat_repeat_mode",
            "TYPE": "long",
            "VALUES": ["All Textures", "Texture 1", "Texture 2", "Texture 3", "Texture 4"],
            "DEFAULT": "All Textures"
        },
        {
            "LABEL": "Tiles/Repeat",
            "NAME": "mat_repeats",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 20,
            "DEFAULT": 5
        },
        {
            "LABEL": "Tiles/Mirror",
            "NAME": "mat_repeat_mirror",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Tiles/Shape",
            "NAME": "mat_tile_shape",
            "TYPE": "long",
            "VALUES": ["Square","Circle"],
            "DEFAULT": "Square"
        },
        {
            "LABEL": "Tiles/Tile Size",
            "NAME": "mat_tile_size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Tiles/Scale Tiles To Fit Region",
            "NAME": "mat_constrain_tiles",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },

        {
            "LABEL": "Tiles/Background",
            "NAME": "mat_tile_back_color",
            "TYPE": "color",
            "DEFAULT": [
                0.0,
                0.0,
                0.0,
                0.0
            ]
        },

        {
            "LABEL": "Texture 1/Texture",
            "NAME": "mat_tex1",
            "TYPE": "image"
        },
        {
            "LABEL": "Texture 1/Aspect",
            "NAME": "mat_t1_aspect",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Texture 1/Scale",
            "NAME": "mat_t1_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Texture 1/Rotate",
            "NAME": "mat_t1_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 1/Shift",
            "NAME": "mat_t1_offset",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },
        {
            "LABEL": "Texture 1/Flip X",
            "NAME": "mat_t1_flip_x",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Texture 1/Flip Y",
            "NAME": "mat_t1_flip_y",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Texture 1/Tile Size",
            "NAME": "mat_t1_tile_size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Texture 2/Texture",
            "NAME": "mat_tex2",
            "TYPE": "image"
        },
        {
            "LABEL": "Texture 2/Aspect",
            "NAME": "mat_t2_aspect",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Texture 2/Scale",
            "NAME": "mat_t2_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Texture 2/Rotate",
            "NAME": "mat_t2_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 2/Shift",
            "NAME": "mat_t2_offset",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },
        {
            "LABEL": "Texture 2/Flip X",
            "NAME": "mat_t2_flip_x",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Texture 2/Flip Y",
            "NAME": "mat_t2_flip_y",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Texture 2/Tile Size",
            "NAME": "mat_t2_tile_size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Texture 3/Texture",
            "NAME": "mat_tex3",
            "TYPE": "image"
        },
        {
            "LABEL": "Texture 3/Aspect",
            "NAME": "mat_t3_aspect",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Texture 3/Scale",
            "NAME": "mat_t3_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Texture 3/Rotate",
            "NAME": "mat_t3_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 3/Shift",
            "NAME": "mat_t3_offset",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },
        {
            "LABEL": "Texture 3/Flip X",
            "NAME": "mat_t3_flip_x",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Texture 3/Flip Y",
            "NAME": "mat_t3_flip_y",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Texture 3/Tile Size",
            "NAME": "mat_t3_tile_size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Texture 4/Texture",
            "NAME": "mat_tex4",
            "TYPE": "image"
        },
        {
            "LABEL": "Texture 4/Aspect",
            "NAME": "mat_t4_aspect",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Texture 4/Scale",
            "NAME": "mat_t4_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Texture 4/Rotate",
            "NAME": "mat_t4_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 4/Shift",
            "NAME": "mat_t4_offset",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },
        {
            "LABEL": "Texture 4/Flip X",
            "NAME": "mat_t4_flip_x",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Texture 4/Flip Y",
            "NAME": "mat_t4_flip_y",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Texture 4/Tile Size",
            "NAME": "mat_t4_tile_size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Tile Shuffle/Enable",
            "NAME": "mat_shuffle_enable",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Tile Shuffle/Mode",
            "NAME": "mat_shuffle_mode",
            "TYPE": "long",
            "VALUES": ["Random 1", "Random 2", "Clockwise Path", "Horizontal Path", "Vertical Path", "Cycle"],
            "DEFAULT": "Random 1"
        },
        {
            "LABEL": "Tile Shuffle/Seed 1",
            "NAME": "mat_seed1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Tile Shuffle/Seed 2",
            "NAME": "mat_seed2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Tile Shuffle/Animate",
            "NAME": "mat_shuffle_animate",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Tile Shuffle/Speed",
            "NAME": "mat_shuffle_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Tile Shuffle/BPM Sync",
            "NAME": "mat_shuffle_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Tile Shuffle/Reverse",
            "NAME": "mat_shuffle_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "LABEL": "Tile Shuffle/Offset",
            "NAME": "mat_shuffle_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Tile Shuffle/Strobe",
            "NAME": "mat_shuffle_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Tile Shuffle/Restart",
            "NAME": "mat_shuffle_restart",
            "TYPE": "event",
        },



        {
            "LABEL": "Tile Size/Animate",
            "NAME": "mat_animate_tile_size",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },

        {
            "LABEL": "Tile Size/Range",
            "NAME": "mat_tile_size_range",
            "TYPE": "floatRange",
            "DEFAULT": [0.0,1.0],
            "MIN": 0.0,
            "MAX": 1.0
        },
        {
            "LABEL": "Tile Size/Region Offset",
            "NAME": "mat_tile_size_region_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.25
        },

        {
            "LABEL": "Tile Size/Signal",
            "NAME": "mat_tile_size_signal",
            "TYPE": "long",
            "VALUES": ["Saw","Inverse Saw","Square","Inverse Square","Triangle","Sine"],
            "DEFAULT": "Saw"
        },
        {
            "LABEL": "Tile Size/Filter",
            "NAME": "mat_tile_size_filter",
            "TYPE": "long",
            "VALUES": ["Ease In","Ease Out","Ease In Out","Ease Out In"],
            "DEFAULT": "Ease In Out"
        },
        {
            "LABEL": "Tile Size/Curve",
            "NAME": "mat_tile_size_curve",
            "TYPE": "float",
            "MIN": 1.0,
            "MAX": 8.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Tile Size/Speed",
            "NAME": "mat_tile_size_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Tile Size/BPM Sync",
            "NAME": "mat_tile_size_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Tile Size/Reverse",
            "NAME": "mat_tile_size_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Tile Size/Offset",
            "NAME": "mat_tile_size_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Tile Size/Strobe",
            "NAME": "mat_tile_size_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Tile Size/Restart",
            "NAME": "mat_tile_size_restart",
            "TYPE": "event",
        },



        {
            "LABEL": "Tile Position/Animate",
            "NAME": "mat_animate_tile_pos",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },



        {
            "LABEL": "Tile Position/Mode",
            "NAME": "mat_tile_pos_mode",
            "TYPE": "long",
            "VALUES": ["Circular","Noise 1", "Noise 2"],
            "DEFAULT": "Circular"
        },

        {
            "LABEL": "Tile Position/Range",
            "NAME": "mat_tile_pos_range",
            "TYPE": "float",
            "DEFAULT": 0.5,
            "MIN": 0.0,
            "MAX": 1.0
        },
        {
            "LABEL": "Tile Position/Region Offset",
            "NAME": "mat_tile_pos_region_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.25
        },
        {
            "LABEL": "Tile Position/Reverse Odd Regions",
            "NAME": "mat_tile_pos_opp_direction",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Tile Position/Speed",
            "NAME": "mat_tile_pos_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Tile Position/BPM Sync",
            "NAME": "mat_tile_pos_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Tile Position/Reverse",
            "NAME": "mat_tile_pos_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Tile Position/Offset",
            "NAME": "mat_tile_pos_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Tile Position/Offset Scale",
            "NAME": "mat_tile_pos_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Tile Position/Strobe",
            "NAME": "mat_tile_pos_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Tile Position/Restart",
            "NAME": "mat_tile_pos_restart",
            "TYPE": "event",
        },

        {
            "LABEL": "Tile Opacity/Animate",
            "NAME": "mat_animate_tile_opacity",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },

        {
            "LABEL": "Tile Opacity/Range",
            "NAME": "mat_tile_opacity_range",
            "TYPE": "floatRange",
            "DEFAULT": [0.0,1.0],
            "MIN": 0.0,
            "MAX": 1.0
        },
        {
            "LABEL": "Tile Opacity/Region Offset",
            "NAME": "mat_tile_opacity_region_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.25
        },

        {
            "LABEL": "Tile Opacity/Signal",
            "NAME": "mat_tile_opacity_signal",
            "TYPE": "long",
            "VALUES": ["Saw","Inverse Saw","Square","Inverse Square","Triangle","Sine"],
            "DEFAULT": "Saw"
        },
        {
            "LABEL": "Tile Opacity/Filter",
            "NAME": "mat_tile_opacity_filter",
            "TYPE": "long",
            "VALUES": ["Ease In","Ease Out","Ease In Out","Ease Out In"],
            "DEFAULT": "Ease In Out"
        },
        {
            "LABEL": "Tile Opacity/Curve",
            "NAME": "mat_tile_opacity_curve",
            "TYPE": "float",
            "MIN": 1.0,
            "MAX": 8.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Tile Opacity/Speed",
            "NAME": "mat_tile_opacity_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Tile Opacity/BPM Sync",
            "NAME": "mat_tile_opacity_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Tile Opacity/Reverse",
            "NAME": "mat_tile_opacity_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Tile Opacity/Offset",
            "NAME": "mat_tile_opacity_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Tile Opacity/Strobe",
            "NAME": "mat_tile_opacity_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Tile Opacity/Restart",
            "NAME": "mat_tile_opacity_restart",
            "TYPE": "event",
        },

        {
            "LABEL": "Scroll/Animate",
            "NAME": "mat_shift_animate",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },

        {
            "Label": "Scroll/Direction",
            "NAME": "mat_shift_angle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 360.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Scroll/Speed",
            "NAME": "mat_shift_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.75
        },
        {
            "LABEL": "Scroll/BPM Sync",
            "NAME": "mat_shift_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Scroll/Reverse",
            "NAME": "mat_shift_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Scroll/Offset",
            "NAME": "mat_shift_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Scroll/Offset Scale",
            "NAME": "mat_shift_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Scroll/Strobe",
            "NAME": "mat_shift_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Scroll/Restart",
            "NAME": "mat_shift_restart",
            "TYPE": "event",
        },

        {
            "LABEL": "Wobble/Animate",
            "NAME": "mat_wobble_animate",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },
        {
            "LABEL": "Wobble/Type",
            "NAME": "mat_wobble_mode",
            "TYPE": "long",
            "VALUES": ["Circular","Noise 1","Noise 2"],
            "DEFAULT": "Circular"
        },
        {
            "Label": "Wobble/Range",
            "NAME": "mat_wobble_range",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.1
        },
        {
            "LABEL": "Wobble/Speed",
            "NAME": "mat_wobble_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
        },
        {
            "LABEL": "Wobble/BPM Sync",
            "NAME": "mat_wobble_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Wobble/Reverse",
            "NAME": "mat_wobble_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Wobble/Offset",
            "NAME": "mat_wobble_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Wobble/Offset Scale",
            "NAME": "mat_wobble_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Wobble/Strobe",
            "NAME": "mat_wobble_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Wobble/Restart",
            "NAME": "mat_wobble_restart",
            "TYPE": "event",
        },

        {
            "LABEL": "Rotate/Animate",
            "NAME": "mat_animate_rotate",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Rotate/Range",
            "NAME": "mat_rotate_range",
            "TYPE": "floatRange",
            "DEFAULT": [0.0,1.0],
            "MIN": 0.0,
            "MAX": 1.0
        },
        {
            "LABEL": "Rotate/Signal",
            "NAME": "mat_rotate_signal",
            "TYPE": "long",
            "VALUES": ["Saw","Inverse Saw","Square","Inverse Square","Triangle","Sine"],
            "DEFAULT": "Saw"
        },

        {
            "LABEL": "Rotate/Filter",
            "NAME": "mat_rotate_filter",
            "TYPE": "long",
            "VALUES": ["Ease In","Ease Out","Ease In Out","Ease Out In"],
            "DEFAULT": "Ease In Out"
        },
        {
            "LABEL": "Rotate/Curve",
            "NAME": "mat_rotate_curve",
            "TYPE": "float",
            "MIN": 1.0,
            "MAX": 8.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Rotate/Speed",
            "NAME": "mat_rotate_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
        },

        {
            "LABEL": "Rotate/BPM Sync",
            "NAME": "mat_rotate_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Rotate/Reverse",
            "NAME": "mat_rotate_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Rotate/Offset",
            "NAME": "mat_rotate_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Rotate/Strobe",
            "NAME": "mat_rotate_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Rotate/Restart",
            "NAME": "mat_rotate_restart",
            "TYPE": "event",

        },

        {
            "LABEL": "Scale/Animate",
            "NAME": "mat_animate_scale",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Scale/Mode",
            "NAME": "mat_scale_mode",
            "TYPE": "long",
            "VALUES": ["Add","Subtract"],
            "DEFAULT": "Add"
        },
        {
            "LABEL": "Scale/Range",
            "NAME": "mat_scale_range",
            "TYPE": "floatRange",
            "DEFAULT": [0.0,1.0],
            "MIN": 0.0,
            "MAX": 1.0
        },
        {
            "LABEL": "Scale/Signal",
            "NAME": "mat_scale_signal",
            "TYPE": "long",
            "VALUES": ["Saw","Inverse Saw","Square","Inverse Square","Triangle","Sine"],
            "DEFAULT": "Saw"
        },
        {
            "LABEL": "Scale/Filter",
            "NAME": "mat_scale_filter",
            "TYPE": "long",
            "VALUES": ["Ease In","Ease Out","Ease In Out","Ease Out In"],
            "DEFAULT": "Ease In Out"
        },
        {
            "LABEL": "Scale/Curve",
            "NAME": "mat_scale_curve",
            "TYPE": "float",
            "MIN": 1.0,
            "MAX": 8.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Scale/Speed",
            "NAME": "mat_scale_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Scale/BPM Sync",
            "NAME": "mat_scale_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Scale/Reverse",
            "NAME": "mat_scale_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Scale/Offset",
            "NAME": "mat_scale_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "Label": "Scale/Strobe",
            "NAME": "mat_scale_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Scale/Restart",
            "NAME": "mat_scale_restart",
            "TYPE": "event",
        },

        {
            "Label": "Region 1/Tile Size",
            "NAME": "mat_r1_tile_size",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Region 1/Shift",
            "NAME": "mat_r1_offset",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },
        {
            "Label": "Region 2/Tile Size",
            "NAME": "mat_r2_tile_size",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Region 2/Shift",
            "NAME": "mat_r2_offset",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },
        {
            "Label": "Region 3/Tile Size",
            "NAME": "mat_r3_tile_size",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Region 3/Shift",
            "NAME": "mat_r3_offset",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },
        {
            "Label": "Region 4/Tile Size",
            "NAME": "mat_r4_tile_size",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Region 4/Shift",
            "NAME": "mat_r4_offset",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },


         {
            "LABEL": "Tile Mask/Enable",
            "NAME": "mat_mask_enable",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Tile Mask/Opacity",
            "NAME": "mat_mask_opacity",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Tile Mask/Texture",
            "NAME": "mat_tex_mask",
            "TYPE": "image"
        },
        {
            "LABEL": "Tile Mask/Aspect",
            "NAME": "mat_mask_aspect",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Tile Mask/Scale",
            "NAME": "mat_mask_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Tile Mask/Rotate",
            "NAME": "mat_mask_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Tile Mask/Shift",
            "NAME": "mat_mask_offset",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },
        {
            "LABEL": "Tile Mask/Flip X",
            "NAME": "mat_mask_flip_x",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Tile Mask/Flip Y",
            "NAME": "mat_mask_flip_y",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },

        {
            "LABEL": "Tile Overlay/Enable",
            "NAME": "mat_overlay_enable",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Tile Overlay/Opacity",
            "NAME": "mat_overlay_opacity",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Tile Overlay/Blend Mode",
            "NAME": "mat_overlay_mode",
            "TYPE": "long",
            "VALUES": ["Add","Over"],
            "DEFAULT": "Add"
        },
        {
            "LABEL": "Tile Overlay/Texture",
            "NAME": "mat_tex_overlay",
            "TYPE": "image"
        },
        {
            "LABEL": "Tile Overlay/Aspect",
            "NAME": "mat_overlay_aspect",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Tile Overlay/Scale",
            "NAME": "mat_overlay_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Tile Overlay/Rotate",
            "NAME": "mat_overlay_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Tile Overlay/Shift",
            "NAME": "mat_overlay_offset",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },
        {
            "LABEL": "Tile Overlay/Flip X",
            "NAME": "mat_overlay_flip_x",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Tile Overlay/Flip Y",
            "NAME": "mat_overlay_flip_y",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },


        {
            "LABEL": "Tile Borders/Enable",
            "NAME": "mat_tile_border_enable",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Tile Borders/Border Thick",
            "NAME": "mat_tile_border_thick",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.25
        },
        {
            "LABEL": "Tile Borders/Border Shape",
            "NAME": "mat_tile_border_shape",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Tile Borders/Border Curve",
            "NAME": "mat_tile_border_curve",
            "TYPE": "float",
            "MIN": 1.0,
            "MAX": 8.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Tile Borders/Border Alpha",
            "NAME": "mat_tile_border_alpha",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },
        {
            "LABEL": "Tile Borders/Mode",
            "NAME": "mat_tile_border_mode",
            "TYPE": "long",
            "VALUES": ["Pre", "Post"],
            "DEFAULT": "Post",
            "FLAGS": "generate_as_define"
        },

        {
            "LABEL": "Texture 1 FX/Enable",
            "NAME": "mat_t1_fx_enable",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Texture 1 FX/Cosine Mix",
            "NAME": "mat_t1_cosine_mix",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 1 FX/Cosine Palette",
            "NAME": "mat_t1_cosine_palette",
            "TYPE": "long",
            "DEFAULT": "Rainbow",
            "VALUES": [ "Gray", "Rainbow", "Blue-Brown", "Blue-Pink", "Savanah","Pink-Brown","Pop","Pinky" ]
        },
        {
            "LABEL": "Texture 1 FX/Cosine Cycle",
            "NAME": "mat_t1_cosine_cycle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 1 FX/Cosine Post Multiply",
            "NAME": "mat_t1_cosine_postmult",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Texture 1 FX/Mono Mix",
            "NAME": "mat_t1_mono_mix",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 1 FX/Mono Color",
            "NAME": "mat_t1_mono_color",
            "TYPE": "color",
            "DEFAULT": [
                0.6,
                0.45,
                0.3,
                1.0
            ]
        },
        {
            "LABEL": "Texture 1 FX/Mono Thresh",
            "NAME": "mat_t1_mono_thresh",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.5
        },
        {
            "LABEL": "Texture 1 FX/Soft BW Mix",
            "NAME": "mat_t1_bw_mix",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 1 FX/Soft BW Gain",
            "NAME": "mat_t1_bw_gain",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Texture 1 FX/Brightness",
            "NAME": "mat_t1_brightness",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 1 FX/Contrast",
            "NAME": "mat_t1_contrast",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1
        },
        {
            "LABEL": "Texture 1 FX/Saturation",
            "NAME": "mat_t1_saturation",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 3.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 1 FX/Hue",
            "NAME": "mat_t1_hue_shift",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 1 FX/Invert",
            "NAME": "mat_t1_invert",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "LABEL": "Texture 2 FX/Enable",
            "NAME": "mat_t2_fx_enable",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Texture 2 FX/Cosine Mix",
            "NAME": "mat_t2_cosine_mix",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 2 FX/Cosine Palette",
            "NAME": "mat_t2_cosine_palette",
            "TYPE": "long",
            "DEFAULT": "Rainbow",
            "VALUES": [ "Gray", "Rainbow", "Blue-Brown", "Blue-Pink", "Savanah","Pink-Brown","Pop","Pinky" ]
        },
        {
            "LABEL": "Texture 2 FX/Cosine Cycle",
            "NAME": "mat_t2_cosine_cycle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 2 FX/Cosine Post Multiply",
            "NAME": "mat_t2_cosine_postmult",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Texture 2 FX/Mono Mix",
            "NAME": "mat_t2_mono_mix",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 2 FX/Mono Color",
            "NAME": "mat_t2_mono_color",
            "TYPE": "color",
            "DEFAULT": [
                0.6,
                0.45,
                0.3,
                1.0
            ]
        },
        {
            "LABEL": "Texture 2 FX/Mono Thresh",
            "NAME": "mat_t2_mono_thresh",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.5
        },
        {
            "LABEL": "Texture 2 FX/Soft BW Mix",
            "NAME": "mat_t2_bw_mix",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 2 FX/Soft BW Gain",
            "NAME": "mat_t2_bw_gain",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Texture 2 FX/Brightness",
            "NAME": "mat_t2_brightness",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 2 FX/Contrast",
            "NAME": "mat_t2_contrast",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1
        },
        {
            "LABEL": "Texture 2 FX/Saturation",
            "NAME": "mat_t2_saturation",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 3.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 2 FX/Hue",
            "NAME": "mat_t2_hue_shift",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 2 FX/Invert",
            "NAME": "mat_t2_invert",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "LABEL": "Texture 3 FX/Enable",
            "NAME": "mat_t3_fx_enable",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Texture 3 FX/Cosine Mix",
            "NAME": "mat_t3_cosine_mix",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 3 FX/Cosine Palette",
            "NAME": "mat_t3_cosine_palette",
            "TYPE": "long",
            "DEFAULT": "Rainbow",
            "VALUES": [ "Gray", "Rainbow", "Blue-Brown", "Blue-Pink", "Savanah","Pink-Brown","Pop","Pinky" ]
        },
        {
            "LABEL": "Texture 3 FX/Cosine Cycle",
            "NAME": "mat_t3_cosine_cycle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 3 FX/Cosine Post Multiply",
            "NAME": "mat_t3_cosine_postmult",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Texture 3 FX/Mono Mix",
            "NAME": "mat_t3_mono_mix",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 3 FX/Mono Color",
            "NAME": "mat_t3_mono_color",
            "TYPE": "color",
            "DEFAULT": [
                0.6,
                0.45,
                0.3,
                1.0
            ]
        },
        {
            "LABEL": "Texture 3 FX/Mono Thresh",
            "NAME": "mat_t3_mono_thresh",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.5
        },
        {
            "LABEL": "Texture 3 FX/Soft BW Mix",
            "NAME": "mat_t3_bw_mix",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 3 FX/Soft BW Gain",
            "NAME": "mat_t3_bw_gain",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Texture 3 FX/Brightness",
            "NAME": "mat_t3_brightness",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 3 FX/Contrast",
            "NAME": "mat_t3_contrast",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1
        },
        {
            "LABEL": "Texture 3 FX/Saturation",
            "NAME": "mat_t3_saturation",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 3.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 3 FX/Hue",
            "NAME": "mat_t3_hue_shift",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 3 FX/Invert",
            "NAME": "mat_t3_invert",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "LABEL": "Texture 4 FX/Enable",
            "NAME": "mat_t4_fx_enable",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Texture 4 FX/Cosine Mix",
            "NAME": "mat_t4_cosine_mix",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 4 FX/Cosine Palette",
            "NAME": "mat_t4_cosine_palette",
            "TYPE": "long",
            "DEFAULT": "Rainbow",
            "VALUES": [ "Gray", "Rainbow", "Blue-Brown", "Blue-Pink", "Savanah","Pink-Brown","Pop","Pinky" ]
        },
        {
            "LABEL": "Texture 4 FX/Cosine Cycle",
            "NAME": "mat_t4_cosine_cycle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 4 FX/Cosine Post Multiply",
            "NAME": "mat_t4_cosine_postmult",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Texture 4 FX/Mono Mix",
            "NAME": "mat_t4_mono_mix",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 4 FX/Mono Color",
            "NAME": "mat_t4_mono_color",
            "TYPE": "color",
            "DEFAULT": [
                0.6,
                0.45,
                0.3,
                1.0
            ]
        },
        {
            "LABEL": "Texture 4 FX/Mono Thresh",
            "NAME": "mat_t4_mono_thresh",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.5
        },
        {
            "LABEL": "Texture 4 FX/Soft BW Mix",
            "NAME": "mat_t4_bw_mix",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 4 FX/Soft BW Gain",
            "NAME": "mat_t4_bw_gain",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Texture 4 FX/Brightness",
            "NAME": "mat_t4_brightness",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 4 FX/Contrast",
            "NAME": "mat_t4_contrast",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1
        },
        {
            "LABEL": "Texture 4 FX/Saturation",
            "NAME": "mat_t4_saturation",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 3.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 4 FX/Hue",
            "NAME": "mat_t4_hue_shift",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Texture 4 FX/Invert",
            "NAME": "mat_t4_invert",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "LABEL": "Color/Brightness",
            "NAME": "mat_brightness",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Color/Contrast",
            "NAME": "mat_contrast",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1
        },
        {
            "LABEL": "Color/Saturation",
            "NAME": "mat_saturation",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 3.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Color/Hue",
            "NAME": "mat_hue_shift",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Color/Invert",
            "NAME": "mat_invert",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "LABEL": "Alpha/Luma to Alpha",
            "NAME": "mat_luma_to_alpha",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Alpha/Sensitivity",
            "NAME": "mat_luma_sensitivity",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 2.0
        },
        {
            "LABEL": "Alpha/Threshold",
            "NAME": "mat_luma_threshold",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.25
        },
        {
            "LABEL": "Alpha/Mode",
            "NAME": "mat_luma_mode",
            "TYPE": "long",
            "VALUES": ["Before Color Controls", "After Color Controls"],
            "DEFAULT": "After Color Controls",
            "FLAGS": "generate_as_define"
        },




    ],
    "GENERATORS": [
        {
            "NAME": "mat_shuffle_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_shuffle_speed",
                "speed_curve":2,
                "reverse": "mat_shuffle_reverse",
                "strob" : "mat_shuffle_strob",
                "reset": "mat_shuffle_restart",
                "bpm_sync": "mat_shuffle_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_shift_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_shift_speed",
                "speed_curve":2,
                "reverse": "mat_shift_reverse",
                "strob" : "mat_shift_strob",
                "reset": "mat_shift_restart",
                "bpm_sync": "mat_shift_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_wobble_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_wobble_speed",
                "speed_curve":2,
                "reverse": "mat_wobble_reverse",
                "strob" : "mat_wobble_strob",
                "reset": "mat_wobble_restart",
                "bpm_sync": "mat_wobble_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_rotate_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_rotate_speed",
                "speed_curve":2,
                "strob" : "mat_rotate_strob",
                "reverse": "mat_rotate_reverse",
                "bpm_sync": "mat_rotate_bpm_sync",
                "reset": "mat_rotate_restart",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_scale_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_scale_speed",
                "speed_curve":2,
                "strob" : "mat_scale_strob",
                "reverse": "mat_scale_reverse",
                "bpm_sync": "mat_scale_bpm_sync",
                "reset": "mat_scale_restart",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_tile_size_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_tile_size_speed",
                "speed_curve":2,
                "reverse": "mat_tile_size_reverse",
                "strob" : "mat_tile_size_strob",
                "reset": "mat_tile_size_restart",
                "bpm_sync": "mat_tile_size_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_tile_pos_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_tile_pos_speed",
                "speed_curve":2,
                "reverse": "mat_tile_pos_reverse",
                "strob" : "mat_tile_pos_strob",
                "reset": "mat_tile_pos_restart",
                "bpm_sync": "mat_tile_pos_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_tile_opacity_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_tile_opacity_speed",
                "speed_curve":2,
                "reverse": "mat_tile_opacity_reverse",
                "strob" : "mat_tile_opacity_strob",
                "reset": "mat_tile_opacity_restart",
                "bpm_sync": "mat_tile_opacity_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        }
    ],
    "IMPORTED": [
        {
            "NAME": "noiseLUT",
            "PATH": "noiseLUT.png",
            "GL_TEXTURE_MIN_FILTER": "LINEAR",
            "GL_TEXTURE_MAG_FILTER": "LINEAR",
            "GL_TEXTURE_WRAP": "REPEAT"
        }
    ]

}*/

/*
By Jason Beyers - October 2023

This material was originally made to display static images (like midjourney images) in a fun format.  But it can certainly be useful for animated textures too!  Play around with the settings - you can do a LOT with this one :)

Given the complexity of supporting 4 textures, this seemed more suitable as a material than as an FX.

FYI: This material has a lot of controls, so it takes a while for Madmapper to load this in a project.  I went a little crazy with the inputs haha.

I tried to implement "fade tiles as they approach the edge of output" logic, but I ran into a brick wall there, and hope to solve that in a future version.  That one may be easier with instancing.
*/

#define NOISE_TEXTURE_BASED
#include "MadCommon.glsl"
#include "MadNoise.glsl"

// int REGIONS = 4;
// int PHASES = 4;

// Timers
// Most of these are modified in other functions
float mat_shuffle_time = fract(mat_shuffle_time_source * 0.125 - mat_shuffle_offset);

float mat_shift_time = (mat_shift_time_source - mat_shift_offset * mat_shift_offset_scale) * 0.05;
float mat_wobble_time = (mat_wobble_time_source - mat_wobble_offset * mat_wobble_offset_scale * 4.);
float mat_rotate_time = fract((mat_rotate_time_source * 0.05  - mat_rotate_offset));
float mat_scale_time = fract(mat_scale_time_source * 0.125 - mat_scale_offset);
float mat_tile_size_time = (mat_tile_size_time_source - mat_tile_size_offset * 2.);
float mat_tile_pos_time = (mat_tile_pos_time_source - mat_tile_pos_offset * 2. * mat_tile_pos_offset_scale);
float mat_tile_opacity_time = (mat_tile_opacity_time_source - mat_tile_opacity_offset * 2.);

// For Cosine Palette FX
const vec3 MAT_ONE3 = vec3(1.);
const vec3 MAT_HALF3 = vec3(0.5);

vec3 mat_palette( in float t, in vec3 a, in vec3 b, in vec3 c, in vec3 d )
{
    return a + b*cos( 6.28318*(c*t+d) );
}

float mat_luma(vec3 color) {
  return dot(color, vec3(0.299, 0.587, 0.114));
}

float mat_luma(vec4 color) {
  return dot(color.rgb, vec3(0.299, 0.587, 0.114));
}

// Easing & filtering functions

float matEaseInOut(float t, float curve) {
    // Simple ease-in-out function
    if (t < 0.5) {
        return pow(2 * t, curve) / 2.;
    } else {
        return 1. - pow(-2. * t + 2, curve) / 2.;
    }
}

float matEaseIn(float t, float curve) {
    // Simple ease-in function
    return pow(t, curve);
}

float matEaseOut(float t, float curve) {
    // Simple ease-out function
    return 1. - pow(1. - t, curve);
}

float matEaseOutIn(float t, float curve) {
    // Ease-out-in function
    // This gets squirrely with high curve values
    return (pow(t, 3.) - 2. * pow(t, 2.) + t) * curve + (-2. * pow(t,3.) + 3. * pow(t, 2.)) + (pow(t, 3.) - pow(t, 2.)) * curve;
}


float matFilter(float t, long filter_type, float curve) {
    // Apply one of four filters to time-varying variable t (ranging from 0 to 1) with curve

    if (filter_type == 0) { // Ease In
        return matEaseIn(t, curve);
    } else if (filter_type == 1) { // Ease Out
        return matEaseOut(t, curve);
    } else if (filter_type == 2) { // Ease In Out
        return matEaseInOut(t, curve);
    } else { // Ease Out In
        return matEaseOutIn(t, curve);
    }
}

// Various helper functions

float matCircle(in vec2 _st, in float _radius){
    vec2 dist = _st-vec2(0.5);
    return 1.-smoothstep(_radius-(_radius*0.01),
                         _radius+(_radius*0.01),
                         dot(dist,dist)*4.0);
}

float getNoise(vec2 p) {
    p += vec2(0.5);
    p *= 0.1;
    p -= vec2(0.5);
    float value = texture(noiseLUT, p).x * 0.5; // Adjust the texture sampler and scale as needed
    // return clamp(value, 0.2, 0.8);
    return value;
}


vec2 uvNoiseOffset(vec2 uv, float time) {
    vec2 noiseOffset = vec2(getNoise(uv + time), getNoise(uv - time));
    return noiseOffset;
}

// UV transform functions

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

vec2 mirrorUV(vec2 uv) {
    uv += vec2(0.5);
    if (mat_mirror_x) {
        if (uv.x > 0.5)   {
            uv.x = 1.0-uv.x;
        }
    }
    if (mat_mirror_y) {
        if (uv.y > 0.5) {
            uv.y = 1.0-uv.y;
        }
    }
    uv -= vec2(0.5);
    return uv;
}

vec2 applyScale(vec2 uv) {
    // Apply UV scale transforms to main output

    if (mat_animate_scale) {
        // scale_time = fract(mat_scale_time);

        float scale_time;

        if (mat_scale_signal == 0) { // Saw
            scale_time = mat_scale_time;
        } else if (mat_scale_signal == 1) { // Inverse Saw
            scale_time = 1. - mat_scale_time;
        } else if (mat_scale_signal == 2) { // Square
            scale_time = floor(mat_scale_time + 0.5);
        } else if (mat_scale_signal == 3) { // Inverse Square
            scale_time = 1. - floor(mat_scale_time + 0.5);
        } else if (mat_scale_signal == 4) { // Triangle
            scale_time = abs(0.5 - mat_scale_time);
        } else { // Sine
            scale_time = 0.5 + 0.5 * sin(2. * PI * mat_scale_time);
        }
        scale_time = matFilter(scale_time, mat_scale_filter, mat_scale_curve);

        scale_time = 1. - scale_time;

        float range_min = mat_scale_range[0];
        float range_max = mat_scale_range[1];
        scale_time = range_min + scale_time * (range_max - range_min);

        if (mat_scale_mode == 0) { // Add
            return uv * mat_scale * (1. + scale_time);
        } else { // Subtract
            return uv * mat_scale / (1. + scale_time);
        }

    } else {
        return uv * mat_scale;
    }
}

vec2 applyUVShift(vec2 uv, float rotate) {
    // Apply UV shift for main output

    vec2 uv_shift = mat_shift_amount * mat_shift_scale * 0.125;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);

    if (mat_shift_animate) {
        float shift_time_x = mat_shift_time * cos(2.*PI * mat_shift_angle/360.0);
        float shift_time_y = mat_shift_time * sin(2.*PI * mat_shift_angle/360.0);
        uv_shift.x -= shift_time_x;
        uv_shift.y -= shift_time_y;
    }
    uv_shift += vec2(0.5);
    uv_shift = matRot2D(uv_shift, 2*PI*(rotate) / 360);
    uv_shift -= vec2(0.5);
    uv += uv_shift;
    return uv;
}

vec2 applyUVWobble(vec2 uv) {
    // Apply UV wobble for main output

    float tile_pos_time;

    if (mat_wobble_animate) {
        float wobble_time = mat_wobble_time;
        vec2 wobble;

        float range_min = 0.;
        float range_max = mat_wobble_range;

        if (mat_wobble_mode == 0) { // Circular

           wobble_time *= 4.;
            float power = 0.5;
            float range = range_min + 1. * (range_max - range_min);
            wobble = 0.25 * power * range * vec2(sin(wobble_time), cos(wobble_time));

        } else if (mat_wobble_mode == 1) { // Noise 1
            wobble_time /= 32.;
            wobble = uvNoiseOffset(vec2(0.5), wobble_time);
            wobble -= vec2(0.5);
            wobble *= 2.;
            wobble.x = range_min + (wobble.x + 0.5) * (range_max - range_min);
            wobble.y = range_min + (wobble.y + 0.5) * (range_max - range_min);
        } else {
            wobble_time /= 1.5;
            range_max /= 32.;
            wobble = dnoise(vec2(wobble_time, 0.)).yz;
            wobble -= vec2(0.5);
            wobble *= 2.;
            wobble.x = range_min + (wobble.x + 0.5) * (range_max - range_min);
            wobble.y = range_min + (wobble.y + 0.5) * (range_max - range_min);
        }
        return uv + wobble;
    } else {
        return uv;
    }
}

float getRotateValue() {
    // Return the rotation value (0-1) for main output

    float rotate;
    float rotate_time;

    if (mat_animate_rotate) {

        if (mat_rotate_signal == 0) { // Saw
            rotate_time = mat_rotate_time;
        } else if (mat_rotate_signal == 1) { // Inverse Saw
            rotate_time = 1. - mat_rotate_time;
        } else if (mat_rotate_signal == 2) { // Square
            rotate_time = floor(mat_rotate_time + 0.5);
        } else if (mat_rotate_signal == 3) { // Inverse Square
            rotate_time = 1. - floor(mat_rotate_time + 0.5);
        } else if (mat_rotate_signal == 4) { // Triangle
            rotate_time = abs(0.5 - mat_rotate_time);
        } else { // Sine
            rotate_time = 0.5 + 0.5 * sin(2. * PI * mat_rotate_time);
        }

        rotate_time = 1. - rotate_time;
        // rotate_time = matEaseInOut(rotate_time, mat_rotate_curve);
        rotate_time = matFilter(rotate_time, mat_rotate_filter, mat_rotate_curve);
        float range_min = mat_rotate_range[0];
        float range_max = mat_rotate_range[1];
        rotate_time = range_min + rotate_time * (range_max - range_min);

        rotate = (rotate_time + mat_rotate / 360.) * 360.;
    } else {
        rotate = mat_rotate;
    }
    return rotate;
}

vec2 transformUV(vec2 uv) {
    // UV transforms for the main output

    if (mat_lock_aspect) {
        uv.x *= RENDERSIZE.x / RENDERSIZE.y;
    }
    uv = applyScale(uv) * 0.5;

    uv = mirrorUV(uv);

    float rotate = getRotateValue();

    // XY shift pre rotate
    if (mat_shift_type == 0) {
        uv = applyUVShift(uv, -1 * rotate);
        uv = applyUVWobble(uv);
    }

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*rotate / 360);
    uv -= vec2(0.5);

    // XY shift post rotate
    if (mat_shift_type == 1) {

        // Preserve direction
        uv = applyUVShift(uv, rotate);
        uv = applyUVWobble(uv);
    }
    return uv;
}

vec2 mat_hat(vec2 uv) {
    // Split the UV space into tiles

    float repeats = mat_repeats;
    if (mat_repeat_mirror) {
        uv = mod(uv * repeats / 2.0, 1.0);
    } else {
        uv = mod(uv * repeats / 2.0, 0.5);
    }

    return 1.0 - 2. * abs(uv - 0.5);
}

vec2 applyGenericUV(vec2 uv, float scale, float aspect, float rotate, vec2 offset, bool flip_x, bool flip_y) {
    // Manipulate UV coordinates in a general way

    uv -= vec2(0.5);
    uv *= scale;
    uv.x *= aspect;
    uv += vec2(0.5);
    if (flip_x) {
        uv.x = 1. - uv.x;
    }
    if (flip_y) {
        uv.y = 1. - uv.y;
    }
    vec2 uv_shift = offset;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);
    uv -= vec2(0.5);
    uv.x /= aspect;
    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*rotate / 360);
    uv -= vec2(0.5);
    uv.x *= aspect;
    uv += vec2(0.5);
    uv_shift += vec2(0.5);
    uv_shift = matRot2D(uv_shift, 2*PI*(rotate) / 360);
    uv_shift -= vec2(0.5);
    uv += uv_shift;
    return uv;
}

// Random functions

float hash(float n) {
    return fract(abs(sin(n * 155.753 * (mat_seed1 + 1.)) * 67.34) + (mat_seed2 + 1.) * 4.);
}

float hash(vec3 p) {
    p = fract(p * 0.1031 * (mat_seed1 + 1.));
    p += dot(p, p.yzx + 19.19 * (mat_seed2 + 1.));
    return fract((p.x + p.y) * p.z);
}

// FX functions (used for textures T1, T2, T3, T4)

vec4 applyColorControls(vec4 color, float brightness, float contrast, float saturation, float hue, bool invert) {
    // Apply color controls FX to the provided vec4 color

    // Apply invert
    if (invert) color.rgb=1-color.rgb;

    // Apply Hue Shift and saturation
    if (hue > 0.01 || saturation != 0) {
        vec3 hsv = rgb2hsv(color.rgb);
        hsv.x = fract(0.9999999*(hsv.x+hue));
        hsv.y = max(hsv.y + saturation, 0);
        color.rgb = hsv2rgb(hsv);
    }

    // Apply contrast
    const vec3 LumCoeff = vec3(0.2125, 0.7154, 0.0721);
    const vec3 AvgLumin = vec3(0.5, 0.5, 0.5);
    vec3 intensity = vec3(dot(color.rgb, LumCoeff));
    color.rgb = mix(AvgLumin, color.rgb, contrast);

    // Apply brightness
    color.rgb += brightness;

    return color;
}

vec4 applyCosinePalette(vec4 color, float fx_mix, long palette, float cycle, bool postmult) {
    // Apply cosine palette FX to the provided vec4 color

    if (fx_mix > 0.0) {
        // original by iquilez https://iquilezles.org/www/articles/palettes/palettes.htm
        vec4 original = color;
        float lum = luma(original.rgb);
        lum = fract(lum + cycle);
        float lumO = lum;

        if(palette == 0) // gray
        { color.rgb = mat_palette(lum,MAT_ONE3,MAT_ONE3,MAT_ONE3,MAT_ONE3);}
        else if(palette == 1) // rainbow
        { color.rgb = mat_palette(lum,MAT_HALF3,MAT_HALF3,MAT_ONE3,vec3(0.,0.33,0.67));}
        else if(palette == 2) // blue-brown
        { color.rgb = mat_palette(lum,MAT_HALF3,MAT_HALF3,MAT_ONE3,vec3(0.,0.1,0.2));}
        else if(palette == 3) // blue-pink
        { color.rgb = mat_palette(lum,MAT_HALF3,MAT_HALF3,MAT_ONE3,vec3(0.3,0.2,0.2));}
        else if(palette == 4) // savanah
        { color.rgb = mat_palette(lum,MAT_HALF3,MAT_HALF3,vec3(1.,1.,0.5),vec3(0.8,0.9,0.3));}
        else if(palette == 5) // pink-brown
        { color.rgb = mat_palette(lum,MAT_HALF3,MAT_HALF3,vec3(1.,0.7,0.4),vec3(0.0,0.15,0.2));}
        else if(palette == 6) // pop
        { color.rgb = mat_palette(lum,MAT_HALF3,MAT_HALF3,vec3(2.,1.,0.),vec3(0.5,0.2,0.25));}
        else if(palette == 7) // pinky
        { color.rgb = mat_palette(lum,vec3(0.8,0.5,0.4),vec3(0.2,0.4,0.2),vec3(2.,1.,1.),vec3(0.,0.25,0.25));}

        if(postmult == true) {
            color.rgb *= vec3(lumO);
        }

        color.rgb = mix(color.rgb,original.rgb,1.-fx_mix);
    }

    return color;
}

vec4 applyColorMonochrome(vec4 color, float fx_mix, vec4 mono_color, float thresh) {
    // Apply color monochrome FX to the provided vec4 color

    if (fx_mix > 0.0) {
        vec4 original = color;
        const vec4  lumcoeff = vec4(0.2126, 0.7152, 0.0722, 0.0);
        float       luminance = dot(original,lumcoeff);
        color = (luminance >= thresh)
            ? mix(mono_color, vec4(1,1,1,1), (luminance-thresh)*2.0)
            : mix(vec4(0,0,0,1), mono_color, luminance*2.0);
        color.rgb = mix(color.rgb, original.rgb,1.-fx_mix);
    }
    return color;
}

vec4 applySoftBW(vec4 color, float fx_mix, float gain) {
    // Apply soft black & white FX to the provided vec4 color
    // This FX can do things that basic saturation & contrast cannot

    if (fx_mix > 0.0) {
        vec4 original = color;
        vec3 raw_color = color.rgb;
        float col_mag = (dot(vec3(1.0), raw_color.rgb) / 3.0 * gain);
        col_mag = smoothstep(0.0, 1.0, col_mag);
        col_mag = smoothstep(0.0, 1.0, col_mag);
        raw_color = vec3(1.0) * col_mag;
        color.rgb = raw_color;
        color.rgb = mix(color.rgb, original.rgb,1.-fx_mix);
    }
    return color;
}

float distToLine(vec2 pt1, vec2 pt2, vec2 testPt) {
    // Helper function for applySoftBorder

    vec2 lineDir = pt2 - pt1;
    vec2 perpDir = vec2(lineDir.y, -lineDir.x);
    vec2 dirToPt1 = pt1 - testPt;
    return abs(dot(normalize(perpDir), dirToPt1));
}


vec4 applySoftBorder(vec4 color, vec2 uv, long tile_shape, float fx_mix, float thick, float shape, float curve) {
    // Apply soft border FX to the provided vec4 color

    if (fx_mix > 0.0) {
        vec4 original = color;
        // uv += vec2(0.5); // ?
        float aspectRatio = 0.5;
        float alphaMult;

        if (tile_shape == 0) { // Square
            vec2 distFromBorder = min(uv,vec2(1)-uv);
            float borderWidthX, borderWidthY;
            if (aspectRatio > 0.5) {
                borderWidthX = thick;
                borderWidthY = thick * (1 - 2*(aspectRatio-0.5));
            } else {
                borderWidthY = thick;
                borderWidthX = thick * (1 - 2*(0.5-aspectRatio));
            }
            float distX=smoothstep(0,borderWidthX*borderWidthX,distFromBorder.x);
            float distY=smoothstep(0,borderWidthY*borderWidthY,distFromBorder.y);
            float dist=mix(min(distX,distY),sqrt(distX*distY),shape);
            alphaMult=dist;
        } else if (tile_shape == 1) { // Circle
            float distFromBorder = 0.5 - length(uv - vec2(0.5,0.5));
            alphaMult = distFromBorder / (thick*thick/2);
        } else { // Triangle
            // Apply transparency on borders
            float distFromBorder = min(distToLine(vec2(0,0),vec2(0.5,1),uv),min(distToLine(vec2(0.5,1),vec2(1,0),uv),distToLine(vec2(1,0),vec2(0,0),uv)));
            alphaMult = distFromBorder / (thick*thick/2);
        }
        if (mat_tile_border_alpha) {
            color.a *= pow(clamp(alphaMult,0,1),curve*curve);
        } else {
            color.rgb *= pow(clamp(alphaMult,0,1),curve*curve);
            color.a = 1.;
        }

        color.rgb = mix(color.rgb, original.rgb,1.-fx_mix);
    }
    return color;
}

// Texture sampling functions

vec4 getT1Color(vec2 uv) {
    // Sample texture 2

    uv = applyGenericUV(uv, mat_t1_scale, mat_t1_aspect, mat_t1_rotate, mat_t1_offset, mat_t1_flip_x, mat_t1_flip_y);
    vec4 color = IMG_NORM_PIXEL(mat_tex1, uv);
    if (mat_t1_fx_enable) {
        color = applyCosinePalette(color, mat_t1_cosine_mix, mat_t1_cosine_palette, mat_t1_cosine_cycle, mat_t1_cosine_postmult);
        color = applyColorMonochrome(color, mat_t1_mono_mix, mat_t1_mono_color, mat_t1_mono_thresh);
        color = applySoftBW(color, mat_t1_bw_mix, mat_t1_bw_gain);
        color = applyColorControls(color, mat_t1_brightness, mat_t1_contrast, mat_t1_saturation, mat_t1_hue_shift, mat_t1_invert);
    }
    return color;
}

vec4 getT2Color(vec2 uv) {
    // Sample texture 2

    uv = applyGenericUV(uv, mat_t2_scale, mat_t2_aspect, mat_t2_rotate, mat_t2_offset, mat_t2_flip_x, mat_t2_flip_y);
    vec4 color = IMG_NORM_PIXEL(mat_tex2, uv);
    if (mat_t2_fx_enable) {
        color = applyCosinePalette(color, mat_t2_cosine_mix, mat_t2_cosine_palette, mat_t2_cosine_cycle, mat_t2_cosine_postmult);
        color = applyColorMonochrome(color, mat_t2_mono_mix, mat_t2_mono_color, mat_t2_mono_thresh);
        color = applySoftBW(color, mat_t2_bw_mix, mat_t2_bw_gain);
        color = applyColorControls(color, mat_t2_brightness, mat_t2_contrast, mat_t2_saturation, mat_t2_hue_shift, mat_t2_invert);
    }
    return color;
}

vec4 getT3Color(vec2 uv) {
    // Sample texture 3

    uv = applyGenericUV(uv, mat_t3_scale, mat_t3_aspect, mat_t3_rotate, mat_t3_offset, mat_t3_flip_x, mat_t3_flip_y);
    vec4 color = IMG_NORM_PIXEL(mat_tex3, uv);
    if (mat_t3_fx_enable) {
        color = applyCosinePalette(color, mat_t3_cosine_mix, mat_t3_cosine_palette, mat_t3_cosine_cycle, mat_t3_cosine_postmult);
        color = applyColorMonochrome(color, mat_t3_mono_mix, mat_t3_mono_color, mat_t3_mono_thresh);
        color = applySoftBW(color, mat_t3_bw_mix, mat_t3_bw_gain);
        color = applyColorControls(color, mat_t3_brightness, mat_t3_contrast, mat_t3_saturation, mat_t3_hue_shift, mat_t3_invert);
    }
    return color;
}

vec4 getT4Color(vec2 uv) {
    // Sample texture 4

    uv = applyGenericUV(uv, mat_t4_scale, mat_t4_aspect, mat_t4_rotate, mat_t4_offset, mat_t4_flip_x, mat_t4_flip_y);
    vec4 color = IMG_NORM_PIXEL(mat_tex4, uv);
    if (mat_t4_fx_enable) {
        color = applyCosinePalette(color, mat_t4_cosine_mix, mat_t4_cosine_palette, mat_t4_cosine_cycle, mat_t4_cosine_postmult);
        color = applyColorMonochrome(color, mat_t4_mono_mix, mat_t4_mono_color, mat_t4_mono_thresh);
        color = applySoftBW(color, mat_t4_bw_mix, mat_t4_bw_gain);
        color = applyColorControls(color, mat_t4_brightness, mat_t4_contrast, mat_t4_saturation, mat_t4_hue_shift, mat_t4_invert);
    }
    return color;
}

vec4 getOverlayColor(vec2 uv) {
    // Sample a custom overlay texture

    uv = applyGenericUV(uv, mat_overlay_scale, mat_overlay_aspect, mat_overlay_rotate,  mat_overlay_offset, mat_overlay_flip_x, mat_overlay_flip_y);
    vec4 color = IMG_NORM_PIXEL(mat_tex_overlay, uv);
    return color;
}

vec4 getMaskColor(vec2 uv) {
    // Sample a custom mask texture

    uv = applyGenericUV(uv, mat_mask_scale, mat_mask_aspect, mat_mask_rotate, mat_mask_offset, mat_mask_flip_x, mat_mask_flip_y);
    vec4 color = IMG_NORM_PIXEL(mat_tex_mask, uv);
    return color;
}

// Functions that manipulate tiles (UV and/or color)

vec4 applyMask(vec4 color, vec2 uv) {
    // Multiply color by mask (luma only, not full color of mask)

    if (mat_mask_enable) {
        vec4 orig_color = color;
        color *= mat_luma(getMaskColor(uv).rgb);
        color = mix(orig_color, color, mat_mask_opacity);
    }
    return color;
}

vec4 overBlend(vec4 color1, vec4 color2) {
    // Implementation of the "Over" blend mode, from ChatGPT

    float alpha1 = color1.a;
    float alpha2 = color2.a;

    vec3 blendedRGB = (color1.rgb * alpha1 + color2.rgb * (1.0 - alpha1 * alpha2)) / (alpha1 + (1.0 - alpha1 * alpha2));
    float blendedAlpha = alpha1 + (1.0 - alpha1 * alpha2);

    return vec4(blendedRGB, blendedAlpha);
}

vec4 applyOverlay(vec4 color, vec2 uv) {
    // Add overlay color to the provided color input

    if (mat_overlay_enable) {
        vec4 overlay_color = getOverlayColor(uv) * mat_overlay_opacity;
        if (mat_overlay_mode == 0) { // Add
            color += overlay_color;
        } else { // Over (only useful when overlay has alpha channel)
            color = overBlend(overlay_color, color);
        }
    }
    return color;
}

vec4 applyTileFX(vec4 color, vec2 uv) {
    // Modify a color input and apply global tile FX

    if (mat_tile_border_enable) {
        color = applySoftBorder(color, uv, mat_tile_shape, 1., mat_tile_border_thick, mat_tile_border_shape, mat_tile_border_curve);
    }
    return color;
}

vec4 applyTileMask(vec4 color, vec2 uv, int tex_num) {
    // Mask the edges if the tile size is < 1

    // Global constraint
    float limit = (1. - mat_tile_size)/2.;

    // Apply texture-specific constraints
    if (tex_num == 1) {
        limit = max(limit, (1. - mat_t1_tile_size)/2.);
    } else if (tex_num == 2) {
        limit = max(limit, (1. - mat_t2_tile_size)/2.);
    } else if (tex_num == 3) {
        limit = max(limit, (1. - mat_t3_tile_size)/2.);
    } else if (tex_num == 4) {
        limit = max(limit, (1. - mat_t4_tile_size)/2.);
    }
    if (mat_tile_shape == 0) { // Square mask
        if (uv.x < limit || uv.y < limit || (1.-uv.x) < limit || (1.-uv.y) < limit) {
            color = mat_tile_back_color;
        }
    } else { // Circle mask
        if (abs(length(vec2(0.5) - uv)) > (1. - 2.*limit) / 2.) {
            color = mat_tile_back_color;
        }
    }
    return color;
}

vec2 applyTileUV(vec2 uv, int tex_num) {
    // Constrain the UV if tile size is < 1 and return the modified UV

    // Global multiplier
    float multiplier = mat_tile_size;

    // Apply texture-specific constraints
    if (tex_num == 1) {
        multiplier = min(multiplier, mat_t1_tile_size);
    } else if (tex_num == 2) {
        multiplier = min(multiplier, mat_t2_tile_size);
    } else if (tex_num == 3) {
        multiplier = min(multiplier, mat_t3_tile_size);
    } else if (tex_num == 4) {
        multiplier = min(multiplier, mat_t4_tile_size);
    }

    uv -= vec2(0.5);
    uv /= multiplier;
    uv += vec2(0.5);
    return uv;
}

float regionTileSize(int region) {
    // Return the size of a given region (1 being full size)
    // If tile size is not animated, return 1

    if (mat_animate_tile_size) {
        float mult = fract(region * mat_tile_size_region_offset - 0.25 + mat_tile_size_time);
        if (mat_tile_size_signal == 0) { // Saw
            mult = mult; // do nothing
        } else if (mat_tile_size_signal == 1) { // Inverse Saw
            mult = 1. - mult;
        } else if (mat_tile_size_signal == 2) { // Square
            mult = floor(mult + 0.5);
        } else if (mat_tile_size_signal == 3) { // Inverse Square
            mult = 1. - floor(mult + 0.5);
        } else if (mat_tile_size_signal == 4) { // Triangle
            mult = fract(region * mat_tile_size_region_offset - 0.25 + mat_tile_size_time / 2.);
            mult = 2. * abs(0.5 - mult);
        } else { // Sine
            mult = 0.5 + 0.5 * (sin(PI*(region * mat_tile_size_region_offset - 0.25 + mat_tile_size_time)));
        }
        mult = matFilter(mult, mat_tile_size_filter, mat_tile_size_curve);
        float range_min = mat_tile_size_range[0];
        float range_max = mat_tile_size_range[1];
        mult = range_min + mult * (range_max - range_min);
        return mult;
    } else {
        return 1.;
    }
}

float regionTileOpacity(int region) {
    // Return a float representing the opacity of a given region
    // If opacity is not animated, always return 1

    if (mat_animate_tile_opacity) {
        float mult = fract(region * mat_tile_opacity_region_offset - 0.25 + mat_tile_opacity_time);
        if (mat_tile_opacity_signal == 0) { // Saw
            mult = mult; // do nothing
        } else if (mat_tile_opacity_signal == 1) { // Inverse Saw
            mult = 1. - mult;
        } else if (mat_tile_opacity_signal == 2) { // Square
            mult = floor(mult + 0.5);
        } else if (mat_tile_opacity_signal == 3) { // Inverse Square
            mult = 1. - floor(mult + 0.5);
        } else if (mat_tile_opacity_signal == 4) { // Triangle
            mult = fract(region * mat_tile_opacity_region_offset - 0.25 + mat_tile_opacity_time / 2.);
            mult = 2. * abs(0.5 - mult);
        } else { // Sine
            mult = 0.5 + 0.5 * (sin(PI*(region * mat_tile_opacity_region_offset - 0.25 + mat_tile_opacity_time)));
        }
        mult = matFilter(mult, mat_tile_opacity_filter, mat_tile_opacity_curve);
        float range_min = mat_tile_opacity_range[0];
        float range_max = mat_tile_opacity_range[1];
        mult = range_min + mult * (range_max - range_min);
        return mult;
    } else {
        return 1.;
    }
}

vec2 regionTilePosition(int region) {
    // Return a vec2 representing an XY offset for tile position, introduced by animation
    // Range of output: [-1,-1] to [1,1]
    // Return vec2(0.) if tile pos animation is disabled

    vec2 pos = vec2(0.);

    if (mat_animate_tile_pos) {

        float tile_pos_time = region * mat_tile_pos_region_offset - 0.25 + mat_tile_pos_time;
        float range_min = 0.;
        float range_max = mat_tile_pos_range;

        if (mat_tile_pos_opp_direction) {
            if (region == 1 || region == 2) {
                tile_pos_time = -1 * tile_pos_time;
            }
        }

        if (mat_tile_pos_mode == 0) { // Circular
            tile_pos_time *= 4.;
            float power = 0.5;
            float range = range_min + 1. * (range_max - range_min);
            pos = 0.25 * power * range * vec2(sin(tile_pos_time), cos(tile_pos_time));

        } else if (mat_tile_pos_mode == 1) { // Noise 1
            tile_pos_time /= 32.;
            pos = uvNoiseOffset(vec2(0.5), tile_pos_time);
            pos -= vec2(0.5);
            pos *= 2.;
            pos.x = range_min + (pos.x + 0.5) * (range_max - range_min);
            pos.y = range_min + (pos.y + 0.5) * (range_max - range_min);
        } else {
            tile_pos_time /= 1.5;
            range_max /= 32.;
            pos = dnoise(vec2(tile_pos_time, region * mat_tile_pos_region_offset)).yz;
            pos -= vec2(0.5);
            pos *= 2.;
            pos.x = range_min + (pos.x + 0.5) * (range_max - range_min);
            pos.y = range_min + (pos.y + 0.5) * (range_max - range_min);
        }
    }
    return pos;
}

vec2 applyRegionUV(vec2 uv, int region) {
    // Apply XY shift and scale modifications to a region's UV coordinates
    // Return a modified vec2 representing new coordinates

    vec2 shift = vec2(0.);
    float multiplier = 1.;
    if (region == 1) {
        shift = mat_r1_offset * 0.5;
        shift += vec2(0.5);
        shift.x = 1.-shift.x;
        shift -= vec2(0.5);
        multiplier = mat_r1_tile_size;
    } else if (region == 2) {
        shift = mat_r2_offset * 0.5;
        shift += vec2(0.5);
        shift.x = 1.-shift.x;
        shift -= vec2(0.5);
        multiplier = mat_r2_tile_size;
    } else if (region == 3) {
        shift = mat_r3_offset * 0.5;
        shift += vec2(0.5);
        shift.x = 1.-shift.x;
        shift -= vec2(0.5);
        multiplier = mat_r3_tile_size;
    } else if (region == 4) {
        shift = mat_r4_offset * 0.5;
        shift += vec2(0.5);
        shift.x = 1.-shift.x;
        shift -= vec2(0.5);
        multiplier = mat_r4_tile_size;
    }

    // Animate the shift, if applicable
    shift += regionTilePosition(region);

    if (mat_constrain_tiles) {
        multiplier /= 2. * length(vec2(0.0) - shift) + 1.;
    }
    multiplier *= regionTileSize(region);
    uv -= vec2(0.5);

    uv /= multiplier;
    uv += vec2(0.5);
    uv += shift;
    return uv;
}

vec4 sampleTexture(vec2 uv, float time, int region, int phase, int tex_num_override) {
    // Sample one of the four textures, depending on shuffle settings, time, screen region, and sequence phase
    // Then, apply the global tile mask & overlay

    vec4 color;
    float select = 0.; // Governs which texture is selected

    if (tex_num_override == 0 && mat_shuffle_enable) {

        if (mat_shuffle_mode == 0) { // Random 1
            select = hash(vec3(time, region, phase));

        } else if (mat_shuffle_mode == 1) { // Random 2
            select = hash(0.25 * region - 0.25 + time);

        } else if (mat_shuffle_mode == 2) { // Clockwise Path
            int region_alt;
            if (region == 1) {
                region_alt = 2;
            } else if (region == 2) {
                region_alt = 1;
            } else {
                region_alt = region;
            }
            select = fract(0.25 * region_alt - 0.25 - time);

         } else if (mat_shuffle_mode == 3) { // Horizontal Path
            int region_alt;
            if (region == 1) {
                region_alt = 3;
            } else if (region == 3) {
                region_alt = 1;
            } else {
                region_alt = region;
            }
            select = fract(0.25 * region_alt - 0.25 - time);

         } else if (mat_shuffle_mode == 4) { // Vertical Path
            int region_alt;
            if (region == 1) {
                region_alt = 4;
            } else if (region == 4) {
                region_alt = 1;
            } else {
                region_alt = region;
            }
            select = fract(0.25 * region_alt - 0.25 - time);

        } else { // Cycle
            select = time;
        }
    } else {
        select = 0.25 * region - 0.25;
    }

    if (region == 1) {
        uv = uv * 2.;
    } else if (region == 2) {
        uv = (uv - vec2(0.,0.5)) * 2.;
    } else if (region == 3) {
        uv = (uv - vec2(0.5,0.)) * 2.;
    } else {
        uv = (uv - vec2(0.5)) * 2.;
    }

    int tex_num;

    if (tex_num_override != 0) {
        tex_num = tex_num_override;
    } else {
        if (select < 0.25) {
            tex_num = 1;
        } else if (select < 0.5) {
            tex_num = 2;
        } else if (select < 0.75) {
            tex_num = 3;
        } else {
            tex_num = 4;
        }
    }

    // Manipulate region and tile UV, to accommodate various user inputs
    uv = applyRegionUV(uv, region);
    vec2 uv_orig = uv;
    uv = applyTileUV(uv, tex_num);

    if (tex_num == 1) {
        color = getT1Color(uv);
    } else if (tex_num == 2) {
        color = getT2Color(uv);
    } else if (tex_num == 3) {
        color = getT3Color(uv);
    } else {
        color = getT4Color(uv);
    }

    if (mat_tile_border_mode == 0) { // Tile FX before mask & overlay
        color = applyTileFX(color, uv);
    }

    color = applyMask(color, uv);
    color = applyOverlay(color, uv);

    vec4 masked_color = applyTileMask(color, uv_orig, tex_num);

    color = masked_color;

    // Modify the opacity, if opacity is being animated
    if (masked_color != mat_tile_back_color) { // Ugly hack to check if a tile mask has obscured something, and only apply opacity animation to the foreground
        color.rgb *= regionTileOpacity(region);
    }

    if (mat_tile_border_mode == 1) { // Tile FX after mask & overlay
        color = applyTileFX(color, uv);
    }

    return color;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    // Global UV transforms
    uv = transformUV(uv);

    uv = mat_hat(uv); // First round of tiling happens here
    // At this point, 1 tile = 4 regions
    // From this point on, "tile" refers to a quarter of that

    int region; // Region of screen (1-4)

    if (uv.x < 0.5 && uv.y < 0.5) {
        region = 1;
    } else if (uv.x < 0.5 && uv.y > 0.5) {
        region = 2;
    } else if (uv.x > 0.5 && uv.y < 0.5) {
        region = 3;
    } else {
        region = 4;
    }

    if (mat_repeat_mode == 0) { // 4 Texture
        if (mat_shuffle_animate) {
            mat_shuffle_time = floor(mat_shuffle_time * 4.0) / 4.0;
        } else {
            mat_shuffle_time = 0.;
        }

        int phase; // Where we are in the sequence

        if (mat_shuffle_time < 0.25) {
            phase = 1;
        } else if (mat_shuffle_time < 0.5) {
            phase = 2;
        } else if (mat_shuffle_time < 0.75) {
            phase = 3;
        } else {
            phase = 4;
        }

        out_color = sampleTexture(uv, mat_shuffle_time, region, phase, 0);

    } else if (mat_repeat_mode == 1) { // Texture 1 only
        out_color = sampleTexture(uv, mat_shuffle_time, region, 0, 1);

    } else if (mat_repeat_mode == 2) { // Texture 2 only
        out_color = sampleTexture(uv, mat_shuffle_time, region, 0, 2);

    } else if (mat_repeat_mode == 3) { // Texture 3 only
        out_color = sampleTexture(uv, mat_shuffle_time, region, 0, 3);

    } else { // Texture 4 only
        out_color = sampleTexture(uv, mat_shuffle_time, region, 0, 4);
    }

    // Luma to alpha (before color controls)
    if (mat_luma_to_alpha && mat_luma_mode == 0) {
        out_color.a = mat_luma(out_color * mat_luma_sensitivity) - mat_luma_threshold * 4. - 1.;
    }

    out_color = applyColorControls(out_color, mat_brightness, mat_contrast, mat_saturation, mat_hue_shift, mat_invert);

    // Luma to alpha (after color controls)
    if (mat_luma_to_alpha && mat_luma_mode == 1) {
        out_color.a = mat_luma(out_color * mat_luma_sensitivity);
    }

    return out_color;
}
