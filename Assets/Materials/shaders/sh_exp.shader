Shader "Custom/sh_exp"
{
	  Properties{
        _O ("Outline", Range(0,1)) = 0.03
        _C ("Color", Color) = (0,0,0,0)
                  _T("_T",Vector) = (0,0,0,0)
    }

    SubShader{
        Tags{ 
		"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
		}
			
			

        Pass{
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM

            #include "UnityCG.cginc"

            #pragma vertex vert
            #pragma fragment frag

			float _O;

            struct appdata{
                float4 vertex : POSITION;
            };

            struct v2f{
                float4 position : SV_POSITION;
				float4 pos:TEXCOORD;
            };

            v2f vert(appdata v){
                v2f o;
                o.position = UnityObjectToClipPos(v.vertex*(1-_O));
				o.pos=mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

			float _V;
			float4 _P;
			float4 _T;
            float4 _C;
            fixed4 frag(v2f i) : SV_TARGET{
			if(_V*_V>=(i.pos.x-_P.x)*(i.pos.x-_P.x)+(i.pos.y-_P.y)*(i.pos.y-_P.y)+(i.pos.z-_P.z)*(i.pos.z-_P.z))
                return fixed4(1, 1, 1, 0);
				else{
                float d = (_T.x - i.pos.x) * (_T.x - i.pos.x) + (_T.y - i.pos.y) * (_T.y - i.pos.y) + (_T.z - i.pos.z) * (_T.z - i.pos.z);
				if ( d<= 25) {
					float tt = 0.5*(sin(-_Time.y * 4 +d))+0.5;
                    return float4(100*(1 + (_C.x - 1) * tt * (25 - d) * 0.04), 100 * (1 + (_C.y - 1) * tt * (25 - d) * 0.04), 100 * (1 + (_C.z - 1) * tt * (25 - d) * 0.04), 1);
				}
				else
				return fixed4(100, 100, 100, 1);
				}
            }

            ENDCG
        }

       Pass{
            Cull front
						Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM

            #include "UnityCG.cginc"

            #pragma vertex vert
            #pragma fragment frag

            float _O;
            struct appdata{
                float4 vertex : POSITION;
            };

            struct v2f{
                float4 position : SV_POSITION;
								float4 pos:TEXCOORD;

            };

            v2f vert(appdata v){
                v2f o;
                o.position = UnityObjectToClipPos(v.vertex);
								o.pos=mul(unity_ObjectToWorld, v.vertex);

                return o;
            }

		 float _V;
		   float4 _P;

            fixed4 frag(v2f i) : SV_TARGET{
			if(_V*_V>=(i.pos.x-_P.x)*(i.pos.x-_P.x)+(i.pos.y-_P.y)*(i.pos.y-_P.y)+(i.pos.z-_P.z)*(i.pos.z-_P.z))
				return fixed4(0,0,0,0.1);
				else
								return fixed4(0,0,0,0.99);
            }

            ENDCG
        }
    }

    FallBack "Standard"
}
