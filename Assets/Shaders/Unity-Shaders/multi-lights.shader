// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/Unity-Shaders/Multi-Lights"
{
	Properties
	{
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_SpecColor("Specular Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_Shininess("Shininess", Float) = 10
		_RimColor("Rim Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_RimPower("Rim Power", Range(0.1, 10)) = 3.0
	}
	SubShader
	{
		Pass
		{
			Tags{ "LightMode" = "ForwardBase" }
			CGPROGRAM
			#pragma vertex		vert
			#pragma fragment	frag
			#include "UnityCG.cginc"

			//user defined variables
			uniform float4 _Color;
			uniform float4 _SpecColor;
			uniform float _Shininess;
			uniform float4 _RimColor;
			uniform float _RimPower;

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

				o.posWorld = mul(unity_ObjectToWorld, v.vertex);
				o.normalDir = normalize(mul(float4(v.normal, 0.0), unity_WorldToObject).xyz);
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

				return o;
			}

			float4 frag(vertexOutput i) : COLOR
			{
				//vectors
				float3 normalDir = i.normalDir;
				float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
				float3 lightDir;
				float atten;

				if (_WorldSpaceLightPos0.w == 0)
				{
					//Directional Lights
					atten = 1.0;
					lightDir = normalize(_WorldSpaceLightPos0.xyz);
				}
				else
				{
					float3 fragmentToLightSource = _WorldSpaceLightPos0.xyz - i.posWorld.xyz;
					float distance = length(fragmentToLightSource);
					atten = 1 / distance;
					lightDir = normalize(fragmentToLightSource);
				}

				//lighting
				float3 diffuseReflect = atten * _LightColor0.xyz * saturate(dot(normalDir, lightDir)) * _Color;
				float3 specularReflect = atten * _LightColor0.xyz * saturate(dot(normalDir, lightDir)) * pow(saturate(dot(viewDir, reflect(-lightDir, normalDir))), _Shininess);

				float rim = 1 - saturate(dot(normalize(viewDir), normalDir));
				float3 rimLighting = atten * _LightColor0.xyz * _RimColor * saturate(dot(normalDir, lightDir) * pow(rim, _RimPower));
				float3 lightFinal = rimLighting + diffuseReflect + specularReflect + UNITY_LIGHTMODEL_AMBIENT;

				return float4(lightFinal, 1.0);
			}

			ENDCG
		}

		Pass
		{
			Tags{ "LightMode" = "ForwardAdd" }
			Blend One One
			CGPROGRAM
			#pragma vertex		vert
			#pragma fragment	frag
			#include "UnityCG.cginc"

			//user defined variables
			uniform float4 _Color;
			uniform float4 _SpecColor;
			uniform float _Shininess;
			uniform float4 _RimColor;
			uniform float _RimPower;

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

				o.posWorld = mul(unity_ObjectToWorld, v.vertex);
				o.normalDir = normalize(mul(float4(v.normal, 0.0), unity_WorldToObject).xyz);
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

				return o;
			}

			float4 frag(vertexOutput i) : COLOR
			{
				//vectors
				float3 normalDir = i.normalDir;
				float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
				float3 lightDir;
				float atten;

				if (_WorldSpaceLightPos0.w == 0)
				{
					//Directional Lights
					atten = 1.0;
					lightDir = normalize(_WorldSpaceLightPos0.xyz);
				}
				else
				{
					float3 fragmentToLightSource = _WorldSpaceLightPos0.xyz - i.posWorld.xyz;
					float distance = length(fragmentToLightSource);
					atten = 1 / distance;
					lightDir = normalize(fragmentToLightSource);
				}

				//lighting
				float3 diffuseReflect = atten * _LightColor0.xyz * saturate(dot(normalDir, lightDir)) * _Color;
				float3 specularReflect = atten * _LightColor0.xyz * saturate(dot(normalDir, lightDir)) * pow(saturate(dot(viewDir, reflect(-lightDir, normalDir))), _Shininess);

				float rim = 1 - saturate(dot(normalize(viewDir), normalDir));
				float3 rimLighting = atten * _LightColor0.xyz * _RimColor * saturate(dot(normalDir, lightDir) * pow(rim, _RimPower));
				float3 lightFinal = rimLighting + diffuseReflect + specularReflect + UNITY_LIGHTMODEL_AMBIENT;

				return float4(lightFinal, 1.0);
			}

			ENDCG
		}
	}
}