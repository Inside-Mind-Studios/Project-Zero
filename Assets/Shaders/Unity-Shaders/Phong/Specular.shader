Shader "Custom/Unity-Shaders/Phong/1 - Specular"
{
	Properties
	{
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_SpecColor("Specular Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_Shininess("Shininess", Float) = 10
	}
	SubShader
	{
		Tags{ "LightMode" = "ForwardBase" }
		Pass
		{
			CGPROGRAM
			#pragma vertex		vert
			#pragma fragment	frag
			#include "UnityCG.cginc"

			//user defined variables
			uniform float4 _Color;
			uniform float4 _SpecColor;
			uniform float _Shininess;

			//Unity defined variables
			uniform float4 _LightColor0;

			struct vertexInput
			{
				float4 vertex: POSITION;
				float3 normal: NORMAL;
			};

			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float4 posWorld: TEX_COORD0;
				float3 normalDir: TEX_COORD1;
			};

			vertexOutput vert(vertexInput v)
			{
				vertexOutput o;

				o.posWorld = mul(_Object2World, v.vertex);
				o.normalDir = normalize(mul(float4(v.normal, 0.0), _World2Object).xyz);
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

				return o;
			}

			float4 frag(vertexOutput i) : COLOR
			{
				//vectors
				float3 normalDir = i.normalDir;
				float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
				float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
				float atten = 1.0;

				//lighting
				float3 diffuseReflect = atten * _LightColor0.xyz * max(0.0, dot(normalDir, lightDir)) * _Color;
				float3 specularReflect = atten * _SpecColor.rgb * max(0.0, dot(normalDir, lightDir)) * pow(max(dot(viewDir, reflect(-lightDir, normalDir)), 0.0), _Shininess);
				float3 lightFinal = diffuseReflect + specularReflect + UNITY_LIGHTMODEL_AMBIENT;

				return float4(lightFinal, 1.0);
			}

			ENDCG
		}
	}
}