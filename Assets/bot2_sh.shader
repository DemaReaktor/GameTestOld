Shader "Custom/bot2_sh"
{
	Properties{
		  _O("Outline", Range(0,1)) = 0.03
		_I("Color",Color)=(0,0,0,1)
	}

		SubShader{
		Tags{ "RenderType" = "Opaque" "Queue" = "Geometry"}

		Pass{
			CGPROGRAM

			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

			float _O;

			struct appdata {
				float4 vertex : POSITION;
			};

			struct v2f {
				float4 position : SV_POSITION;
			};

			v2f vert(appdata v) {
				v2f o;
				o.position = UnityObjectToClipPos(v.vertex*(1 - _O)*float4(0.25*cos(_Time.y*0.5)+0.75,0.25*cos(_Time.y*0.5) + 0.75,0.25*cos(_Time.y*0.5) + 0.75,1));
				return o;
			}

			float4 _I;
		    bool _T;

			fixed4 frag(v2f i) : SV_TARGET{
			if(!_T){
			//_T=true;
							return _I;
			}
			else
							return _I*float4(1,1,1,0);
			}

			ENDCG
		}	

	    Pass{
			Cull front

			CGPROGRAM

				#include "UnityCG.cginc"

				#pragma vertex vert
				#pragma fragment frag

			struct appdata {
				float4 vertex : POSITION;
				float2 uv:TEXCOORD;
			};

			struct v2f {
				float4 position : SV_POSITION;
			};

			v2f vert(appdata v) {
				v2f o;
				o.position = UnityObjectToClipPos(v.vertex);
				return o;
			}

			float4 _I;
			fixed4 frag(v2f i) : SV_TARGET{
				return fixed4(_I.x*0.5*(1 - cos(_Time.y*0.5)),_I.y*0.5*(1-cos(_Time.y*0.5)),_I.z*0.5*(1 - cos(_Time.y*0.5)),1);
			}

			ENDCG
		}
	}
	
				FallBack "Standard"
}
