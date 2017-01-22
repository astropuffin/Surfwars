// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Hidden/Water"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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
				float4 vertex : SV_POSITION;
			};

			float4 lowerLeft;
			float4 upperRight;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float heights[100];
			float xLeft, xRight, yLeft, yRight;


			float4 frag (v2f i) : SV_Target
			{
				float2 worldPos;
				worldPos.x = lerp(lowerLeft.x, upperRight.x, -i.uv.x);
				worldPos.y = lerp(lowerLeft.y, upperRight.y, i.uv.y);

				float cameraWidth = upperRight.x - lowerLeft.x;

				worldPos.x += cameraWidth;

				float dist = (xRight - xLeft )  + 1;
				float xUV = (xLeft  - worldPos.x ) / dist;
				xUV *= -1;

				float lerpAmount = frac(xUV * 100);
				int leftIndex = (int)(xUV * 100);
				int rightIndex = (int)(xUV * 100) + 1;

				float myHeight = lerp(heights[leftIndex], heights[rightIndex], lerpAmount);
				if (leftIndex < 1)
				{
					myHeight = heights[1];
				}
				if (rightIndex >= 99)
				{
					myHeight = heights[98];
				}


				float distortion = 0;
				float depth = myHeight - worldPos.y;

				if (depth >= 0)
				{
					distortion = sin(depth) / 100;
				}

				i.uv.x += distortion;
				fixed4 col = tex2D(_MainTex, i.uv );


				if ( depth >= 0 )
				{
					depth += 1.9;
					col.b += .5f;
					col /= (depth ) / 2;
				}
				 
				return col;
			}
			ENDCG
		}
	}
}
