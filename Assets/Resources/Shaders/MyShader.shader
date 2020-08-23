Shader "Myshader"
{
    SubShader
    {
       

        CGPROGRAM
        //surface表示定义的着色器类型为surface，着色器色入口方法为Surf(surfaceFunction),光照模型BlinnPhong
#pragma surface  surf BlinnPhong
        struct Input
        {
            float3 viewDir;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            //albedo光源的反射率
            //o.Albedo = float4(1,0,0,1);
            o.Emission = float4(0, 1, 0, 1);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
