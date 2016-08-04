Shader "Custom/Unity-Shaders/Gouraud/5 - Specular-Cg" 
{
	Properties
	{

	}
	SubShader
	{
		Pass
		{
			Tags{ "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			struct appin
			{
				float4 Position : POSITION;
				float4 Normal	: NORMAL;
			};

			struct vertout
			{
				float4 HPosition : POSITION;
				float4 Color	 : COLOR;
			};

			vertout vert(appin IN)
			{
				vertout OUT;

				// Transform vertex position into homogenous clip-space
				OUT.HPosition = mul(UNITY_MATRIX_MVP, IN.Position);

				// Transform normal from model-space to view-space
				float3 normalVec = normalize(mul(IN.Normal, _World2Object).xyz);

				// Store normalized light vector.
				float3 lightVec = normalize(_WorldSpaceLightPos0.xyz);

				// Calculate Half Angle Vector
				float3 eyeVec = float3(0.0, 0.0, 1.0);
				float3 halfVec = normalize(lightVec + eyeVec);

				// Caclulate Diffuse Component
				float diffuse = dot(normalVec, lightVec);

				// Caclulate Specular Component
				float specular = dot(normalVec, halfVec);

				// Use the lit function to compute lighting vector from diffuse and specular values
				float4 lighting = lit(diffuse, specular, 32);

				// Blue diffuse material
				float3 diffuseMaterial = float3(0.0, 0.0, 1.0);

				// White specular material
				float3 specularMaterial = float3(1.0, 1.0, 1.0);

				// Combine diffuse and specular contributions and output the final vertex color
				OUT.Color.rgb = lighting.y * diffuseMaterial + lighting.z * specularMaterial;
				OUT.Color.a = 1.0;

				return OUT;
			}

			float4 frag(vertout IN) : COLOR
			{
				return IN.Color;
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
