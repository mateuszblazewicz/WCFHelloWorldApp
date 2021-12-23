from locust import HttpUser, task, between, tag

class TestWcfIIS(HttpUser):
    host = 'http://20.63.65.130'
    wait_time = between(0.2, 0.3)

    def on_start(self):
        self.client.verify = False

    @task
    @tag("helper")
    def iis_get_data(self):
        self.client.get("/WcfTest/wcfhelper/GetData?type=iis")
    
    @task
    @tag("nohelper")
    def iis_get_data2(self):
        self.client.get("/WcfTest/GetData?type=iis")