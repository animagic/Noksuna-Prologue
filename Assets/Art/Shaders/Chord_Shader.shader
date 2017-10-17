// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Noksuna/Abilities/Bolt_Shader"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_MaskClipValue( "Mask Clip Value", Float ) = 0.5
		_Albedo("Albedo", 2D) = "white" {}
		_Normals("Normals", 2D) = "bump" {}
		_Mask("Mask", 2D) = "white" {}
		_specular("specular", 2D) = "white" {}
		_FX("FX", 2D) = "white" {}
		_Float0("Float 0", Range( 0 , 25)) = 0
		_Color("Color", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha noshadow exclude_path:deferred vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
			float2 texcoord_0;
		};

		uniform sampler2D _Normals;
		uniform float4 _Normals_ST;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float4 _Color;
		uniform sampler2D _Mask;
		uniform float4 _Mask_ST;
		uniform sampler2D _FX;
		uniform float _Float0;
		uniform sampler2D _specular;
		uniform float4 _specular_ST;
		uniform float _MaskClipValue = 0.5;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			o.texcoord_0.xy = v.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normals = i.uv_texcoord * _Normals_ST.xy + _Normals_ST.zw;
			o.Normal = UnpackNormal( tex2D( _Normals, uv_Normals ) );
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			o.Albedo = ( tex2D( _Albedo, uv_Albedo ) * _Color ).xyz;
			float2 uv_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			o.Emission = ( ( tex2D( _Mask, uv_Mask ) * ( float4( 0,0,0,0 ) + tex2D( _FX, (abs( i.texcoord_0+_Time.x * float2(1,1 ))) ) ) ) * ( _Float0 * ( _SinTime.x + 1.5 ) ) ).xyz;
			float2 uv_specular = i.uv_texcoord * _specular_ST.xy + _specular_ST.zw;
			float4 tex2DNode11 = tex2D( _specular, uv_specular );
			o.Metallic = tex2DNode11.x;
			o.Smoothness = tex2DNode11.x;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13101
224;94;1279;603;1094.023;471.275;1.54394;True;False
Node;AmplifyShaderEditor.TimeNode;16;-1124.505,556.4352;Float;False;0;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.TextureCoordinatesNode;15;-1219.505,409.4352;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.PannerNode;14;-931.505,350.4352;Float;False;1;1;2;0;FLOAT2;0,0;False;1;FLOAT;0.0;False;1;FLOAT2
Node;AmplifyShaderEditor.SamplerNode;12;-645.1736,520.45;Float;True;Property;_FX;FX;5;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SinTimeNode;20;-325.8126,648.3684;Float;False;0;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;10;-627.4493,283.2726;Float;True;Property;_Mask;Mask;3;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;19;25.18744,714.3684;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;1.5;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;18;-171.1918,639.4321;Float;False;Property;_Float0;Float 0;6;0;0;0;25;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;40;-236.54,488.956;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.SamplerNode;8;-651.9059,-439.1698;Float;True;Property;_Albedo;Albedo;1;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;44;-267.9378,-260.6966;Float;False;Property;_Color;Color;7;0;0,0,0,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;362.8078,642.1168;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-87.52213,356.3953;Float;False;2;2;0;FLOAT4;0.0;False;1;FLOAT4;0.0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;43;31.29498,-295.6436;Float;False;2;2;0;FLOAT4;0.0;False;1;COLOR;0.0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.ColorNode;41;-337.4348,170.5276;Float;False;Constant;_Color1;Color 1;7;0;1,1,1,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;9;-636.4653,-200.6152;Float;True;Property;_Normals;Normals;2;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;158.6965,399.5605;Float;False;2;2;0;FLOAT4;0.0;False;1;FLOAT;0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.SamplerNode;11;-652.4958,49.29773;Float;True;Property;_specular;specular;4;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;513.0129,168.3323;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Noksuna/Abilities/Bolt_Shader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Custom;0.5;True;False;0;True;TransparentCutout;Transparent;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;False;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0.4044118,0,0,0;VertexOffset;False;Spherical;False;Relative;0;;0;-1;-1;-1;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;14;0;15;0
WireConnection;14;1;16;1
WireConnection;12;1;14;0
WireConnection;19;0;20;1
WireConnection;40;1;12;0
WireConnection;17;0;18;0
WireConnection;17;1;19;0
WireConnection;13;0;10;0
WireConnection;13;1;40;0
WireConnection;43;0;8;0
WireConnection;43;1;44;0
WireConnection;21;0;13;0
WireConnection;21;1;17;0
WireConnection;0;0;43;0
WireConnection;0;1;9;0
WireConnection;0;2;21;0
WireConnection;0;3;11;0
WireConnection;0;4;11;0
ASEEND*/
//CHKSM=BA3BBB4CD4AFF4846F1798EEC67AF724C721C27C