from locust import HttpUser, task, between, tag

class TestWcfAKS(HttpUser):
    host = 'http://20.200.78.212'
    wait_time = between(0.2, 0.3)

    def on_start(self):
        self.client.verify = False

    @task
    @tag("helper")
    def aks_get_data(self):
        self.client.get("/WcfTest/wcfhelper/GetData?type=aks")

    @task
    @tag("nohelper")
    def aks_get_data2(self):
        self.client.get("/WcfTest/GetData?type=aks")