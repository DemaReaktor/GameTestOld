Shader "Custom/text_sh"
{
	Properties
	{
		//[PerRendererData] не показує
		 _MainTex("Sprite Texture", 2D) = "white" {}
		_R("Vec",Vector) = (0,0,0,0)
		_Col("Color",Color) = (0,0,0,0)
		_R1("Range",Range(0,1)) = 0
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}

			Cull Off
			Lighting Off
			ZWrite Off
			ZTest[unity_GUIZTestMode]
			Blend SrcAlpha OneMinusSrcAlpha

			Pass
			{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"
				#include "UnityUI.cginc"

				struct appdata_t
				{
					float4 vertex   : POSITION;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					half2 texcoord  : TEXCOORD0;
				};

				v2f vert(appdata_t IN)
				{
					v2f OUT;
					OUT.vertex = UnityObjectToClipPos(IN.vertex);

					OUT.texcoord = IN.texcoord;
					return OUT;
				}

				sampler2D _MainTex;
				float4 _R;
				float4 _Col;
				float _R1;
				fixed4 frag(v2f IN) : SV_Target
				{
					float r = 1 -0.0003*((_R.x-IN.vertex.x)* (_R.x - IN.vertex.x)+ (_R.y - IN.vertex.y)* (_R.y - IN.vertex.y));
				float4 v = tex2D(_MainTex, IN.texcoord);
				if (r < 0)
					r = 0;
				return float4(r+ v.x+_R1, v.y, v.z,v.w)+_Col;
				}
			ENDCG
			}
		}
}