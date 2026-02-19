package com.studysaas.app.repository

import com.studysaas.app.BuildConfig
import com.studysaas.app.api.AuthInterceptor
import com.studysaas.app.api.StudyApiService
import okhttp3.OkHttpClient
import okhttp3.logging.HttpLoggingInterceptor
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

object ApiClient {
    private var jwtToken: String? = null

    fun setToken(token: String) {
        jwtToken = token
    }

    fun api(): StudyApiService {
        val logging = HttpLoggingInterceptor().apply { level = HttpLoggingInterceptor.Level.BODY }
        val httpClient = OkHttpClient.Builder()
            .addInterceptor(AuthInterceptor(BuildConfig.TENANT_CODE) { jwtToken })
            .addInterceptor(logging)
            .build()

        return Retrofit.Builder()
            .baseUrl(BuildConfig.API_BASE_URL)
            .client(httpClient)
            .addConverterFactory(GsonConverterFactory.create())
            .build()
            .create(StudyApiService::class.java)
    }
}
