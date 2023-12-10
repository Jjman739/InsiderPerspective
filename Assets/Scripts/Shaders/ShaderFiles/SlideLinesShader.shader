Shader "Hidden/SlideLinesShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Frequency("Size", float) = 1.0
		_Speed("Horizontal Location", float) = 1.0
        _RadiansCounterClockwise("Radians Counter Clockwise", float) = 0.0
		_TrippyMode("Trippy Mode", Integer) = 0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            uniform float _Frequency;
		    uniform float _Speed;
            uniform float _RadiansCounterClockwise;
		    uniform int _TrippyMode;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed3 colorMultiples = fixed3(0.0, 0.0, 0.0);

                // Get current time multiplied with the speed
                float time = _Time * _Speed;

                // Takes into account the following:
                // _ScreenParams.xy - aspect ratio to make sure lines are same size no matter the direction or screen size
                // i.uv.xy - Current xy position on the screen
                // degreesCounterClockwise - angle for the lines to move in
                // frequency - number of lines
                const float param = (i.uv.x * _ScreenParams.x * cos(_RadiansCounterClockwise) + i.uv.y * _ScreenParams.y * sin(_RadiansCounterClockwise)) * _Frequency / 16.0;
                
                // Trippy Mode (Move rgb channels at different speeds)
                colorMultiples.r += _TrippyMode * (sin(param + (time*30)) + 1.0) / 2.0;
                colorMultiples.g += _TrippyMode * (sin(param + (time*40)) + 1.0) / 2.0;
                colorMultiples.b += _TrippyMode * (sin(param + (time*50)) + 1.0) / 2.0;

                // Not Trippy Mode (All colors are same speed (causes black line))
                colorMultiples.rgb += !_TrippyMode * (sin(param + (time*30)) + 1.0) / 2.0;
                
                col.rgb *= colorMultiples;

                return col;
            }
            ENDCG
        }
    }
}
