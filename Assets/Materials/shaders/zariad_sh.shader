Shader "Custom/zariad_sh"
{
	Properties
	{
		//[PerRendererData] не показує
		 _MainTex("Sprite Texture", 2D) = "white" {}
		_R("vidsotok",Range(0,1))=0.00
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

				float _R;
				v2f vert(appdata_t IN)
				{
					v2f OUT;
					OUT.vertex = UnityObjectToClipPos(IN.vertex);

					OUT.texcoord = IN.texcoord;
					return OUT;
				}
				
				sampler2D _MainTex;
				fixed4 frag(v2f IN) : SV_Target
				{
					float4 v = tex2D(_MainTex, IN.texcoord);
					return float4((1-_R)*v.y,v.y-1+_R,0,v.y);
				}
			ENDCG
			}
		}
}