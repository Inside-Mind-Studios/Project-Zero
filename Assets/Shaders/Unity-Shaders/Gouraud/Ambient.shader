// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/Unity-Shaders/Gouraud/3 - Ambient"
{
	Properties
	{
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
	}
		SubShader
	{
		Pass
	{
		Tags{ "LightMode" = "ForwardBase" }
		CGPROGRAM

#pragma vertex		vert
#pragma fragment	frag

		uniform float4 _Color;

	//Unity defined variables
	uniform float4 _LightColor0;

	struct vertexInput
	{
		float4 vertex: POSITION;
		float3 normal: NORMAL;
	};

	struct vertexOutput
	{
		float4 pos: SV_POSITION;
		float4 col: COLOR;
	};

	vertexOutput vert(vertexInput v)
	{
		vertexOutput o;

		float3 normalDir = normalize(mul(float4(v.normal, 1.0), unity_WorldToObject).xyz);
		float3 lightDir;
		float atten = 1.0;

		lightDir = normalize(_WorldSpaceLightPos0.xyz);

		float3 diffuseReflect = atten * _LightColor0.xyz * max(0.0f, dot(normalDir, lightDir)) * _Color;
		float3 lightFinal = diffuseReflect + UNITY_LIGHTMODEL_AMBIENT.xyz;

		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.col = float4(lightFinal, 1.0);
		return o;
	}

	float4 frag(vertexOutput i) : COLOR
	{
		return i.col;
	}

		ENDCG
	}
	}
		//Fallback "Something"
}