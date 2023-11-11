// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Russian HD House/GlassDistortion"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_Color("Color", Color) = (0.7941176,0.7941176,0.7941176,0)
		_Albedo("Albedo", 2D) = "white" {}
		_MetallicSmoothness("MetallicSmoothness", 2D) = "black" {}
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_NormalMap("Normal Map", 2D) = "bump" {}
		_GlobalNormals("Global Normals", 2D) = "bump" {}
		_GNormals("GNormals", Range( 0 , 1)) = 0.426465
		_Distortion("Distortion", Range( 0 , 2)) = 1.812023
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[Header(Refraction)]
		_ChromaticAberration("Chromatic Aberration", Range( 0 , 0.3)) = 0.1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Transparent+0" }
		Cull Back
		GrabPass{ "RefractionGrab0" }
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma multi_compile _ALPHAPREMULTIPLY_ON
		#pragma surface surf StandardSpecular keepalpha finalcolor:RefractionF  noshadow 
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
			float3 worldPos;
		};

		uniform float _GNormals;
		uniform sampler2D _GlobalNormals;
		uniform float4 _GlobalNormals_ST;
		uniform float _Distortion;
		uniform sampler2D _NormalMap;
		uniform float4 _NormalMap_ST;
		uniform float4 _Color;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform sampler2D _MetallicSmoothness;
		uniform float4 _MetallicSmoothness_ST;
		uniform float _Smoothness;
		uniform sampler2D RefractionGrab0;
		uniform float _ChromaticAberration;

		inline float4 Refraction( Input i, SurfaceOutputStandardSpecular o, float indexOfRefraction, float chomaticAberration ) {
			float3 worldNormal = o.Normal;
			float4 screenPos = i.screenPos;
			#if UNITY_UV_STARTS_AT_TOP
				float scale = -1.0;
			#else
				float scale = 1.0;
			#endif
			float halfPosW = screenPos.w * 0.5;
			screenPos.y = ( screenPos.y - halfPosW ) * _ProjectionParams.x * scale + halfPosW;
			#if SHADER_API_D3D9 || SHADER_API_D3D11
				screenPos.w += 0.0000001;
			#endif
			float2 projScreenPos = ( screenPos / screenPos.w ).xy;
			float3 worldViewDir = normalize( UnityWorldSpaceViewDir( i.worldPos ) );
			float3 refractionOffset = ( ( ( ( indexOfRefraction - 1.0 ) * mul( UNITY_MATRIX_V, float4( worldNormal, 0.0 ) ) ) * ( 1.0 / ( screenPos.z + 1.0 ) ) ) * ( 1.0 - dot( worldNormal, worldViewDir ) ) );
			float2 cameraRefraction = float2( refractionOffset.x, -( refractionOffset.y * _ProjectionParams.x ) );
			float4 redAlpha = tex2D( RefractionGrab0, ( projScreenPos + cameraRefraction ) );
			float green = tex2D( RefractionGrab0, ( projScreenPos + ( cameraRefraction * ( 1.0 - chomaticAberration ) ) ) ).g;
			float blue = tex2D( RefractionGrab0, ( projScreenPos + ( cameraRefraction * ( 1.0 + chomaticAberration ) ) ) ).b;
			return float4( redAlpha.r, green, blue, redAlpha.a );
		}

		void RefractionF( Input i, SurfaceOutputStandardSpecular o, inout fixed4 color )
		{
			#ifdef UNITY_PASS_FORWARDBASE
				color.rgb = color.rgb + Refraction( i, o, _Distortion, _ChromaticAberration ) * ( 1 - color.a );
				color.a = 1;
			#endif
		}

		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			float2 uv_GlobalNormals = i.uv_texcoord * _GlobalNormals_ST.xy + _GlobalNormals_ST.zw;
			float2 uv_NormalMap = i.uv_texcoord * _NormalMap_ST.xy + _NormalMap_ST.zw;
			o.Normal = BlendNormals( UnpackScaleNormal( tex2D( _GlobalNormals,uv_GlobalNormals) ,_GNormals ) , UnpackScaleNormal( tex2D( _NormalMap,uv_NormalMap) ,_Distortion ) );
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float4 tex2DNode14 = tex2D( _Albedo,uv_Albedo);
			o.Albedo = ( _Color * tex2DNode14 ).rgb;
			float2 uv_MetallicSmoothness = i.uv_texcoord * _MetallicSmoothness_ST.xy + _MetallicSmoothness_ST.zw;
			float3 componentMask18 = tex2D( _MetallicSmoothness,uv_MetallicSmoothness).rgb;
			o.Specular = componentMask18;
			o.Smoothness = _Smoothness;
			o.Alpha = ( _Color.a + tex2DNode14.a );
			o.Normal = o.Normal + 0.00001 * i.screenPos * i.worldPos;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=7003
1927;29;1666;1014;1486.369;825.4705;1.6;True;True
Node;AmplifyShaderEditor.RangedFloatNode;9;-2603.703,537.6419;Float;False;Property;_Distortion;Distortion;7;0;1.812023;0;2;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;22;-2685.846,91.55138;Float;False;Property;_GNormals;GNormals;6;0;0.426465;0;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;26;-840.3741,-488.7493;Float;False;Property;_Color;Color;0;0;0.7941176,0.7941176,0.7941176,0;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;14;-978.7786,-229.3587;Float;True;Property;_Albedo;Albedo;1;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;17;-914.1638,526.3094;Float;True;Property;_MetallicSmoothness;MetallicSmoothness;2;0;None;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;1;-2248.576,280.4782;Float;True;Property;_NormalMap;Normal Map;4;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;19;-2254.56,45.36653;Float;True;Property;_GlobalNormals;Global Normals;5;0;Assets/AmplifyShaderEditor/Examples/Community/HologramSimple/HoloNormal.png;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;0.2;False;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ComponentMaskNode;18;-467.1317,551.9246;Float;False;True;True;True;False;0;COLOR;0,0,0,0;False;FLOAT3
Node;AmplifyShaderEditor.BlendNormalsNode;21;-1564.588,86.01881;Float;False;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;FLOAT3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-336.2732,-31.84941;Float;False;0;COLOR;0.0,0,0,0;False;1;COLOR;0.0;False;COLOR
Node;AmplifyShaderEditor.RangedFloatNode;23;-282.4289,933.4606;Float;False;Constant;_Float0;Float 0;6;0;0;0;0;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;25;-364.2677,-142.5766;Float;False;Property;_Smoothness;Smoothness;3;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;36;-54.36829,-364.6709;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;342.4001,-8.000003;Float;False;True;2;Float;ASEMaterialInspector;0;StandardSpecular;Russian HD House/GlassDistortion;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Translucent;0.5;True;False;0;False;Opaque;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;False;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0.23;0,0,0,0;VertexOffset;False;Cylindrical;Relative;0;;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;OBJECT;0.0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False
WireConnection;1;5;9;0
WireConnection;19;5;22;0
WireConnection;18;0;17;0
WireConnection;21;0;19;0
WireConnection;21;1;1;0
WireConnection;27;0;26;0
WireConnection;27;1;14;0
WireConnection;36;0;26;4
WireConnection;36;1;14;4
WireConnection;0;0;27;0
WireConnection;0;1;21;0
WireConnection;0;3;18;0
WireConnection;0;4;25;0
WireConnection;0;8;9;0
WireConnection;0;9;36;0
ASEEND*/
//CHKSM=BB9F29EA7C50230D77B69ED00809D320253FCCA4