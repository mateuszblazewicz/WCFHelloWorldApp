from locust import HttpUser, task, between, tag

class TestWcfAppService(HttpUser):
    host = 'http://rbcwcftestclient.azurewebsites.net'
    wait_time = between(0.2, 0.3)

    def on_start(self):
        self.client.verify = False

    @task
    @tag("helper")
    def appservice_get_data(self):
        self.client.get("/WcfTest/wcfhelper/GetData?type=appservice")

    @task
    @tag("nohelper")
    def appservice_get_data2(self):
        self.client.get("/WcfTest/GetData?type=appservice")