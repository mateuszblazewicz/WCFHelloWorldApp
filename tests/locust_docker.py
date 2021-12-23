from locust import HttpUser, task, between, tag

class TestWcfDocker(HttpUser):
    host = 'http://20.116.31.140'
    wait_time = between(0.2, 0.3)

    def on_start(self):
        self.client.verify = False

    @task    
    @tag("helper")
    def dockervm_get_data(self):
        self.client.get("/WcfTest/wcfhelper/GetData?type=docker")

    @task    
    @tag("nohelper")
    def dockervm_get_data2(self):
        self.client.get("/WcfTest/GetData?type=docker")