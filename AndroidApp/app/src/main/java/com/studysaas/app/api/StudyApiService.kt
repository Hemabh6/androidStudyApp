package com.studysaas.app.api

import com.studysaas.app.model.AuthResponse
import com.studysaas.app.model.Course
import com.studysaas.app.model.LoginRequest
import com.studysaas.app.model.TestDto
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.Path

interface StudyApiService {
    @POST("api/auth/login")
    fun login(@Body request: LoginRequest): Call<AuthResponse>

    @GET("api/courses")
    fun getCourses(): Call<List<Course>>

    @GET("api/tests/course/{courseId}")
    fun getTests(@Path("courseId") courseId: String): Call<List<TestDto>>
}
