// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/Unity-Shaders/Gouraud/4 - Specular"
{
	Properties
	{
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_SpecColor("Specular Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_Shininess("Shininess", Float) = 10
	}
	SubShader
	{
		Tags { "LightMode" = "ForwardBase" }
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
				float4 col: COLOR;
			};

			vertexOutput vert(vertexInput v)
			{
				vertexOutput o;

				//vectors
				float3 normalDir = normalize(mul(float4(v.normal, 0.0), unity_WorldToObject).xyz);
				float3 viewDir = normalize(float3(float4(_WorldSpaceCameraPos.xyz, 1.0) - mul(unity_ObjectToWorld, v.vertex).xyz));
				float3 lightDir;
				float atten = 1.0;

				//lighting
				lightDir = normalize(_WorldSpaceLightPos0.xyz);
				float3 diffuseReflect = atten * _LightColor0.xyz * max(0.0, dot(normalDir, lightDir));
				float3 specularReflect = atten * _SpecColor.rgb * max(0.0, dot(normalDir, lightDir)) * pow(max(0.0, dot(reflect(-lightDir, normalDir), viewDir)), _Shininess);
				float3 lightFinal = diffuseReflect + specularReflect + UNITY_LIGHTMODEL_AMBIENT;

				o.col = float4(lightFinal * _Color, 1.0);
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

				return o;
			}

			float4 frag(vertexOutput i) : COLOR
			{
				return i.col;
			}

			ENDCG
		}
	}
}