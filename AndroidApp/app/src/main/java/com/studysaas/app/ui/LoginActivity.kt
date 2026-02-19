package com.studysaas.app.ui

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import com.studysaas.app.R
import com.studysaas.app.model.AuthResponse
import com.studysaas.app.model.LoginRequest
import com.studysaas.app.repository.ApiClient
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class LoginActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_login)

        val emailEdit = findViewById<EditText>(R.id.emailEdit)
        val passwordEdit = findViewById<EditText>(R.id.passwordEdit)
        val statusText = findViewById<TextView>(R.id.statusText)
        val loginButton = findViewById<Button>(R.id.loginButton)

        emailEdit.setText("student@demo.com")
        passwordEdit.setText("Pass@123")

        loginButton.setOnClickListener {
            statusText.text = "Signing in..."
            ApiClient.api().login(LoginRequest(emailEdit.text.toString(), passwordEdit.text.toString()))
                .enqueue(object : Callback<AuthResponse> {
                    override fun onResponse(call: Call<AuthResponse>, response: Response<AuthResponse>) {
                        if (!response.isSuccessful || response.body() == null) {
                            statusText.text = "Login failed (${response.code()})"
                            return
                        }

                        val auth = response.body()!!
                        ApiClient.setToken(auth.token)
                        statusText.text = "Welcome ${auth.fullName}"

                        startActivity(Intent(this@LoginActivity, CoursesActivity::class.java))
                    }

                    override fun onFailure(call: Call<AuthResponse>, t: Throwable) {
                        statusText.text = "Network error: ${t.message}"
                    }
                })
        }
    }
}
