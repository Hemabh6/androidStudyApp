package com.studysaas.app.ui

import android.os.Bundle
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import com.studysaas.app.R
import com.studysaas.app.model.TestDto
import com.studysaas.app.repository.ApiClient
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class TestActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_test)

        val statusText = findViewById<TextView>(R.id.testStatus)
        val courseId = intent.getStringExtra("COURSE_ID")

        if (courseId.isNullOrBlank()) {
            statusText.text = "Invalid course"
            return
        }

        statusText.text = "Loading tests..."

        ApiClient.api().getTests(courseId).enqueue(object : Callback<List<TestDto>> {
            override fun onResponse(call: Call<List<TestDto>>, response: Response<List<TestDto>>) {
                if (!response.isSuccessful || response.body() == null) {
                    statusText.text = "Unable to fetch tests (${response.code()})"
                    return
                }

                val tests = response.body()!!
                val text = buildString {
                    append("Tests available: ${tests.size}\n\n")
                    tests.forEach { test ->
                        append("• ${test.title}\n")
                        test.questions.forEachIndexed { index, q ->
                            append("   Q${index + 1}: ${q.prompt}\n")
                        }
                        append("\n")
                    }
                }
                statusText.text = text
            }

            override fun onFailure(call: Call<List<TestDto>>, t: Throwable) {
                statusText.text = "Error: ${t.message}"
            }
        })
    }
}
