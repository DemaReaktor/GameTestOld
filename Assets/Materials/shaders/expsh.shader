Shader "Custom/expsh"
{
	Properties{
		//  _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
		  _O("Outline", Range(0,1)) = 0.03
		  //_I("OutlineI", Range(0,3)) = 0.03
		_I("Color",Color)=(0,0,0,1)
// _Color ("Tint", Color) = (0, 0, 0, 1)
  _M("Texture", 2D) = "white" {}
	}

		SubShader{
		//the material is completely non-transparent and is rendered at the same time as the other opaque geometry
		Tags{ "RenderType" = "Opaque" "Queue" = "Geometry"}

		//The first pass where we render the Object itself
		Pass{
			CGPROGRAM

			//include useful shader functions
			#include "UnityCG.cginc"

			//define vertex and fragment shader
			#pragma vertex vert
			#pragma fragment frag

			//texture and transforms of the texture
			//sampler2D _MainTex;
			//float4 _MainTex_ST;
			float _O;
			//tint of the texture
		   // fixed4 _Color;

			//the object data that's put into the vertex shader
			struct appdata {
				float4 vertex : POSITION;
				float2 uv_M : TEXCOORD0;
				//float4 normal : NORMAL;
			};

			//the data that's used to generate fragments and can be read by the fragment shader
			struct v2f {
				float4 position : SV_POSITION;
				float4 uv : TEXCOORD;
				float4 uv1 : TEXCOORD1;
				float4 uv2 : TEXCOORD2;
			};

			//the vertex shader
			v2f vert(appdata v) {
				v2f o;
				//convert the vertex positions from object space to clip space so they can be rendered
				//float4 f = o.position;
				//v.vertex *=float4(1, sin(_Time.y),1,1);
				o.position = UnityObjectToClipPos(v.vertex*(1 - _O)*float4(0.25*cos(_Time.y*0.5)+0.75,0.25*cos(_Time.y*0.5) + 0.75,0.25*cos(_Time.y*0.5) + 0.75,1));
				o.uv = mul(unity_ObjectToWorld,v.uv_M)+float4(-sin(_Time.y*0.2),0.5,0,0);
				o.uv1 = mul(unity_ObjectToWorld,v.uv_M)+float4(0.3,sin(_Time.y*0.8)*0.3,0,0);
				o.uv2 = mul(unity_ObjectToWorld,v.uv_M)+float4(cos(_Time.y*0.45)*0.5,cos(_Time.y*0.65)*0.2-1,0,0);
				return o;
			}

			sampler2D _M;
			float4 _I;
			fixed4 frag(v2f i) : SV_TARGET{
				fixed4 col = tex2D(_M, i.uv);
				fixed4 col1 = tex2D(_M, i.uv1);
				fixed4 col2 = tex2D(_M, i.uv2);
			float4 c=_I*col*col1*col2;
				return float4(c.r,c.g,1-c.r,1);
			}

			ENDCG
		}	

	    Pass{
			Cull front

			CGPROGRAM

				#include "UnityCG.cginc"

				#pragma vertex vert
				#pragma fragment frag

				float _O;
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
