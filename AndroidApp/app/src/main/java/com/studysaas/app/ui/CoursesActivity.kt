package com.studysaas.app.ui

import android.content.Intent
import android.os.Bundle
import android.widget.ArrayAdapter
import android.widget.ListView
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import com.studysaas.app.R
import com.studysaas.app.model.Course
import com.studysaas.app.repository.ApiClient
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class CoursesActivity : AppCompatActivity() {
    private var courses: List<Course> = emptyList()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_courses)

        val status = findViewById<TextView>(R.id.courseStatus)
        val listView = findViewById<ListView>(R.id.courseList)

        status.text = "Loading courses..."

        ApiClient.api().getCourses().enqueue(object : Callback<List<Course>> {
            override fun onResponse(call: Call<List<Course>>, response: Response<List<Course>>) {
                if (!response.isSuccessful || response.body() == null) {
                    status.text = "Failed to fetch courses (${response.code()})"
                    return
                }

                courses = response.body()!!
                status.text = "Courses (${courses.size})"

                listView.adapter = ArrayAdapter(
                    this@CoursesActivity,
                    android.R.layout.simple_list_item_1,
                    courses.map { it.title }
                )

                listView.setOnItemClickListener { _, _, position, _ ->
                    val course = courses[position]
                    startActivity(Intent(this@CoursesActivity, TestActivity::class.java).putExtra("COURSE_ID", course.id))
                }
            }

            override fun onFailure(call: Call<List<Course>>, t: Throwable) {
                status.text = "Error: ${t.message}"
            }
        })
    }
}
