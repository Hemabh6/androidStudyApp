package com.studysaas.app.model

data class LoginRequest(val email: String, val password: String)
data class AuthResponse(
    val token: String,
    val userId: String,
    val instituteId: String,
    val role: String,
    val fullName: String
)

data class Course(
    val id: String,
    val instituteId: String,
    val title: String,
    val description: String,
    val teacherId: String
)

data class TestQuestion(
    val id: String,
    val prompt: String,
    val options: List<String>
)

data class TestDto(
    val id: String,
    val instituteId: String,
    val courseId: String,
    val title: String,
    val questions: List<TestQuestion>
)
