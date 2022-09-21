Shader "Custom/prominf_sh"
{
	Properties{
	//_A("Alpha",Range(0,1)) = 0
	_V("pos",Vector) = (0,0,0,0)
  _Color("Color", Color) = (0, 0, 0, 1)

  _MainTex("Texture", 2D) = "white" {}
	}
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard alpha
					//				#include "UnityCG.cginc"
				//#pragma fragment frag

//#pragma vertex vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
	//float _A;
	float4 _V;
        struct Input
        {
            float2 uv_MainTex;
        };

	
        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) *_Color;
            o.Albedo = c.rgb;
			float d = 1;// sin(Time.y + sqrt((i.c.x - _V.x)*(i.c.x - _V.x) + (i.c.y - _V.y)*(i.c.y - _V.y) + (i.c.z - _V.z)*(i.c.z - _V.z)));
			if (d >= 0.5)
				o.Alpha = 1;
			else
				o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
