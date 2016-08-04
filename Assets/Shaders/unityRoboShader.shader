Shader	"Custom/Unity/Robot"
{
	// Data (variables) for materials to input
	Properties 
	{
		_MainTexture("Main Color (RGB) Hello!", 2D) = "white" {}
		_Color("Kleur", Color) = (1,1,1,1)
		_DissolveTexture("Cheese", 2D) = "white" {}
		_DissolveAmount("Cheese Intensity", Range(0, 1)) = 1

		_ExtrudeAmount("Extrude Amount", Range(-0.1, 0.1)) = 0
	}
	
	// Sub Shaders, you can have multiple sub shaders for different platforms
	SubShader
	{
		// Pass, takes the data and draws it to the screen (can have multiple passes)
		// Use as few passes as possible, it's good practice!
		Pass
		{
			CGPROGRAM	// Start CG

			// Define functions and include any headers if necessary
			#pragma vertex		vertexFunction
			#pragma fragment	fragmentFunction
			#include "UnityCG.cginc"

			// Vertices, Normals, Color, UV
			// In CG vecn objects are replaced by floatn objects 
			// x,y,z,w  |  r,g,b,a  |  s,t,p,q
			struct appData
			{
				float4 vertex: POSITION;
				float2 uv: TEXCOORD;
				float3 normal: NORMAL;
			};

			struct v2f
			{
				float4 position: SV_POSITION;
				float2 uv: TEXCOORD0;
			};

			float4 _Color;
			sampler2D _MainTexture;
			float _DissolveAmount;
			sampler2D _DissolveTexture;
			float _ExtrudeAmount;

			// Vertex, Build the object
			v2f vertexFunction(appData IN)
			{
				v2f OUT;
				IN.vertex.xyz += IN.normal.xyz * _ExtrudeAmount * sin(_Time.y);
				OUT.position = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.uv = IN.uv;
				return OUT;
			}

			// Fragment, Color the Object
			fixed4 fragmentFunction(v2f IN): SV_TARGET 
			{
				float4 textureColor = tex2D(_MainTexture, IN.uv);
				float4 dissolveColor = tex2D(_DissolveTexture, IN.uv);
				//clip(dissolveColor.rgb - _DissolveAmount);

				return textureColor * _Color;
			}

			ENDCG		// End CG
		}
	}
}