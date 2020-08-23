Shader "Custom/Shader02"
{
    Properties
    {
        // 声明一个Color类型的属性
      _Color("Color Tint",Color) = (1.0,1.0,1.0,1.0)
    }

        SubShader
    {
        Pass
       {

        CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members pos)
#pragma exclude_renderers d3d11

        #pragma vertex vert
        #pragma fragment frag

        fixed4 _Color;
        struct a2v
    {
        //用POSITION语义告unity，用模型空间的顶点坐标来填充vertex变量
        float4 vertex : POSITION;
        //用NORMAL语义告诉unity，用模型空间的法线方向来填充normal变量
        float3 normal : NORMAL;
        //用TEXCOORD0语义告诉unity，用模型的第一套纹理坐标来填充texcoord的变量
        float4 texcoord :  TEXCOORD0
    };

    //使用一个结构体来定义顶点着色器的输出
    struct v2f
    {
        //SV_POSITION语义告诉unity ，pos里包含了顶点在裁剪空间中的位置信息
        float4 pos ：SV_POSITION;
        //COLOR0语义可以用于存储颜色信息
        fixed3 clor : COLOR0;

    };

    float4 vert(a2v v) : POSITION
    {
        v2f o; //声明输出结构
        o.pos = mul(UNITY_MATRIX - MVP, v.vertex);
        //return mul(UNITY_MATRIX - MVP, v.vertex);

        //把分量范围映射到[0.0,1.0]
        //存储到o.color中传递给片元着色器
        o.color = v.normal * 0.5 + fixed3(0.5, 0.5, 0.5, 0.5);

        return o;
    }

        fixed4 frag(v2f i) : SV_Target
    {
        //将插值后的i.color显示到屏幕上
        //return fixed4(1.0,1.0,1.0,1.0);

        fixed3 c = i.color;
    c *= _Color.rgb;
    return fixed4(c, 1.0);

        }
        ENDCG
            }
    }
   
}
