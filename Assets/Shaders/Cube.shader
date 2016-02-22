// Shader created with Shader Forge Beta 0.34 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.34;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,blpr:0,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:True,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32633,y:32713|emission-77-OUT,voffset-81-OUT;n:type:ShaderForge.SFN_Color,id:4,x:33418,y:32529,ptlb:node_4,ptin:_node_4,glob:False,c1:0.07569203,c2:0.866,c3:0.7080001,c4:1;n:type:ShaderForge.SFN_Color,id:10,x:33493,y:32692,ptlb:node_7_copy,ptin:_node_7_copy,glob:False,c1:0.1012111,c2:0.691,c3:0.7647059,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:26,x:33622,y:33246,ptlb:Barre,ptin:_Barre,glob:False,v1:4;n:type:ShaderForge.SFN_Tau,id:29,x:33648,y:33101;n:type:ShaderForge.SFN_Time,id:31,x:34004,y:32622;n:type:ShaderForge.SFN_NormalVector,id:32,x:33335,y:33144,pt:False;n:type:ShaderForge.SFN_ValueProperty,id:34,x:33093,y:33270,ptlb:Ondulation,ptin:_Ondulation,glob:False,v1:0.5;n:type:ShaderForge.SFN_ValueProperty,id:36,x:33856,y:32520,ptlb:Temps,ptin:_Temps,glob:False,v1:0.1;n:type:ShaderForge.SFN_Multiply,id:37,x:33797,y:32656|A-36-OUT,B-31-T;n:type:ShaderForge.SFN_TexCoord,id:75,x:34112,y:32847,uv:0;n:type:ShaderForge.SFN_Add,id:76,x:33839,y:32863|A-37-OUT,B-75-U;n:type:ShaderForge.SFN_Lerp,id:77,x:33047,y:32636|A-4-RGB,B-10-RGB,T-80-OUT;n:type:ShaderForge.SFN_Sin,id:78,x:33407,y:32887|IN-79-OUT;n:type:ShaderForge.SFN_Multiply,id:79,x:33634,y:32887|A-76-OUT,B-29-OUT,C-26-OUT;n:type:ShaderForge.SFN_RemapRange,id:80,x:33234,y:32887,frmn:-1,frmx:1,tomn:0,tomx:1|IN-78-OUT;n:type:ShaderForge.SFN_Multiply,id:81,x:32969,y:32990|A-80-OUT,B-32-OUT,C-34-OUT;proporder:4-10-26-34-36;pass:END;sub:END;*/

Shader "Custom/Cube" {
    Properties {
        _node_4 ("node_4", Color) = (0.07569203,0.866,0.7080001,1)
        _node_7_copy ("node_7_copy", Color) = (0.1012111,0.691,0.7647059,1)
        _Barre ("Barre", Float ) = 4
        _Ondulation ("Ondulation", Float ) = 0.5
        _Temps ("Temps", Float ) = 0.1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _node_4;
            uniform float4 _node_7_copy;
            uniform float _Barre;
            uniform float _Ondulation;
            uniform float _Temps;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                float4 node_31 = _Time + _TimeEditor;
                float node_80 = (sin((((_Temps*node_31.g)+o.uv0.r)*6.28318530718*_Barre))*0.5+0.5);
                v.vertex.xyz += (node_80*v.normal*_Ondulation);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Normals:
                float3 normalDirection =  i.normalDir;
////// Lighting:
////// Emissive:
                float4 node_31 = _Time + _TimeEditor;
                float node_80 = (sin((((_Temps*node_31.g)+i.uv0.r)*6.28318530718*_Barre))*0.5+0.5);
                float3 emissive = lerp(_node_4.rgb,_node_7_copy.rgb,node_80);
                float3 finalColor = emissive;
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCollector"
            Tags {
                "LightMode"="ShadowCollector"
            }
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCOLLECTOR
            #define SHADOW_COLLECTOR_PASS
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcollector
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _Barre;
            uniform float _Ondulation;
            uniform float _Temps;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_COLLECTOR;
                float2 uv0 : TEXCOORD5;
                float3 normalDir : TEXCOORD6;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                float4 node_31 = _Time + _TimeEditor;
                float node_80 = (sin((((_Temps*node_31.g)+o.uv0.r)*6.28318530718*_Barre))*0.5+0.5);
                v.vertex.xyz += (node_80*v.normal*_Ondulation);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_COLLECTOR(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                SHADOW_COLLECTOR_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Cull Off
            Offset 1, 1
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _Barre;
            uniform float _Ondulation;
            uniform float _Temps;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                float4 node_31 = _Time + _TimeEditor;
                float node_80 = (sin((((_Temps*node_31.g)+o.uv0.r)*6.28318530718*_Barre))*0.5+0.5);
                v.vertex.xyz += (node_80*v.normal*_Ondulation);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
