from locust import HttpUser, task

class TestWcf(HttpUser):
    wait_time = constant(0.3)

    def on_start(self):
        self.client.verify = False

    @task
    def get_data(self):
        self.client.get("/WcfTest/GetData")