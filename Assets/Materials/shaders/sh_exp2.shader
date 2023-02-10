Shader "Custom/sh_exp2"
{
	Properties{
		  _C("Color", Color) = (0,0,0,0)
		  _T("_T",Vector)=(0,0,0,0)
	}
		SubShader{
		Pass{

Cull front
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM

			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

			struct appdata {
				float4 vertex : POSITION;
			};

			struct v2f {
				float4 position : SV_POSITION;
				float4 pos:TEXCOORD;
			};

			v2f vert(appdata v) {
				v2f o;
                o.position = UnityObjectToClipPos(v.vertex);
				o.pos=mul(unity_ObjectToWorld, v.vertex);				
				return o;
			}
			float4 _T;
			float4 _C;
			fixed4 frag(v2f i) : SV_TARGET{
			float tt;
			float d = (_T.x - i.pos.x) * (_T.x - i.pos.x) + (_T.z - i.pos.z) * (_T.z - i.pos.z) + (_T.y - i.pos.y) * (_T.y - i.pos.y);
				tt=sin(-_Time.y*4+d);
				tt=0.5*tt+0.5;
				if(d<=25)
					return float4(1+(_C.x -1)* tt*(25-d)*0.04, 1 + (_C.y - 1) * tt * (25 - d) * 0.04, 1 + (_C.z - 1) * tt * (25 - d) * 0.04, 1);
				else
				return float4(1,1,1,1);
			}

			ENDCG
		}
	}

				FallBack "Standard"
}
