Shader "Custom/zariad_sh"
{
	Properties
	{
		//[PerRendererData] не показує
		_R("rANGE",Range(0,360))=0.00
				_R2("rANGE2",Range(0,1)) = 0.00
		 _MainTex("Sprite Texture", 2D) = "white" {}
	//	[PerRendererData] _V("vect",Vector[]) = {}//(0,0,0,0)
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
						float4 _MainTex_ST;

				struct appdata_t
				{
					float4 vertex   : POSITION;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv:TEXCOORD1;
					float4 vertex   : SV_POSITION;
					half2 texcoord  : TEXCOORD0;
				};

				v2f vert(appdata_t IN)
				{
					v2f OUT;
					OUT.vertex = UnityObjectToClipPos(IN.vertex);
					OUT.uv = TRANSFORM_TEX(IN.texcoord, _MainTex);

					OUT.texcoord = IN.texcoord;
					return OUT;
				}

				sampler2D _MainTex;
				float _R;
				float _R2;
				float4 _V[100];
				float _N;
				//float4 _Z;
				fixed4 frag(v2f IN) : SV_Target
				{
					float gr = _R /57.3 ;
					int f,f1;

					if(_R>60&&_R<=240)
					f=-1;
					else
					f=1;
					if(_R>=120&&_R<300)
					f1=-1;
					else
					f1=1;

					if (f*IN.uv.y >=f*( tan(gr+0.52)*(IN.uv.x - 0.5) + 0.5)&&f1*IN.uv.y >= f1*(tan(gr-0.52)*(IN.uv.x - 0.5) + 0.5)) {
					if(_R2*_R2*0.25>=(IN.uv.y-0.5)*(IN.uv.y-0.5)+(IN.uv.x-0.5)*(IN.uv.x-0.5)||(IN.uv.y-0.5)*(IN.uv.y-0.5)+(IN.uv.x-0.5)*(IN.uv.x-0.5)>=0.25)
		return tex2D(_MainTex, IN.texcoord);
		else{
		float4 c=tex2D(_MainTex, IN.texcoord);
		return float4(1-c.r,1-c.g,1-c.b,_R2*c.a);
		}
		}
		else{
		int k=0;
				if((IN.uv.x-0.5)*(IN.uv.x-0.5)+(IN.uv.y-0.5)*(IN.uv.y-0.5)<=_N*_N/16&&(IN.uv.x-0.5)*(IN.uv.x-0.5)+(IN.uv.y-0.5)*(IN.uv.y-0.5)<=0.25){
								//if((IN.uv.x-0.5-_Z.x)*(IN.uv.x-0.5-_Z.x)+(IN.uv.y-0.5-_Z.z)*(IN.uv.y-0.5-_Z.z)<=0.001||k==3)
		//k=3;
		//else
		for(int i=0;i<100;i++)
		if((IN.uv.x-0.5-_V[i].x)*(IN.uv.x-0.5-_V[i].x)+(IN.uv.y-0.5-_V[i].z)*(IN.uv.y-0.5-_V[i].z)<=0.0002||k==2)
		k=2;
		else
		k=1;
		}
		//if(k==3)
		//return float4(0,0,0,1);
		//else
		if(k==2)
return float4(1,0,0,1);
else
if(k==1)
return float4(0,1,0.3,0.25+0.25*tan(-_Time.y*2+20*((IN.uv.x-0.5)*(IN.uv.x-0.5)+(IN.uv.y-0.5)*(IN.uv.y-0.5))));
else
		return float4(0,0,0,0);}
		}
			ENDCG
			}
		}
}