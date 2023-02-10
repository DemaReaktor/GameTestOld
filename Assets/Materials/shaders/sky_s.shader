Shader "Custom/sky_s"
{
	Properties
	{
		 _M("Sprite Texture", Color) = (0,0,0,0)
	}

		SubShader
		{
			Tags
			{
			"Queue" = "Background" "RenderType" = "Background" "PreviewType" = "Skybox"
			}

			Cull Off
			ZWrite Off

		CGINCLUDE
				#include "UnityCG.cginc"

		float3 RotateAroundYInDegrees(float3 vertex)
	{
		float sina, cosa;
		sincos(0, sina, cosa);
		float2x2 m = float2x2(cosa, -sina, sina, cosa);
		return float3(mul(m, vertex.xz), vertex.y).xzy;
	}

				  struct appdata_t {
		float4 vertex : POSITION;
		float2 texcoord : TEXCOORD0;
		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

	struct v2f {
		float4 vertex : SV_POSITION;
		float2 texcoord : TEXCOORD0;
		UNITY_VERTEX_OUTPUT_STEREO
	};

	v2f vert(appdata_t v)
	{
		v2f o;
		UNITY_SETUP_INSTANCE_ID(v);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
		o.vertex = UnityObjectToClipPos(RotateAroundYInDegrees(v.vertex));
		o.texcoord = v.texcoord;
		return o;
	}

			ENDCG

				Pass{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma target 2.0

					float4 _M;

		half4 frag(v2f i) : SV_Target { return _M;	}
		ENDCG
			}


			}
		}
