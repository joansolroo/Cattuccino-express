Shader "Unlit/LiquidShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Density ("Density", Range(0, 1.0)) = 0.5
		_Color ("Color", Color) = (1, 1, 1, 1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

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
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;		

			float _Density;
			half4 _Color;
			uniform sampler2D _LiquidTex;
			uniform float4 _LiquidTex_TexelSize;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = float4(0,0,0,1);
				fixed4 lcol = tex2D(_LiquidTex, i.uv);
				if (lcol.a > _Density) {
					float2 delta = float2(_LiquidTex_TexelSize.x,_LiquidTex_TexelSize.y);
					float3 c = float3(0,0,0);
					for(int x = 1; x<=1;++x){
						for(int y = 1;y<=1;++y){
							fixed4 lcol2 = tex2D(_LiquidTex, i.uv+(x*delta.x,y*delta.y));
							col.rgb += lcol2.rgb/9.0;
							//c.rgb += lcol2/9;//tex2D(_LiquidTex, i.uv/*+(x*delta.x,y*delta.y)*/).rgb/9;
						}
					}
					col.rgb = col.rgb*9.0;
					//col.rgb = lcol.rgb;
					//col.a =1;
				} else {
					col = tex2D(_MainTex, i.uv);
				}
					
				return col;
			}
			ENDCG
		}
	}
}
