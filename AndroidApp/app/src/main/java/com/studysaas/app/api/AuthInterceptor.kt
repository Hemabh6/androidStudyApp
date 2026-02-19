package com.studysaas.app.api

import okhttp3.Interceptor
import okhttp3.Response

class AuthInterceptor(
    private val tenant: String,
    private val tokenProvider: () -> String?
) : Interceptor {
    override fun intercept(chain: Interceptor.Chain): Response {
        val original = chain.request()
        val requestBuilder = original.newBuilder()
            .addHeader("X-Tenant", tenant)

        tokenProvider()?.let { token ->
            requestBuilder.addHeader("Authorization", "Bearer $token")
        }

        return chain.proceed(requestBuilder.build())
    }
}
