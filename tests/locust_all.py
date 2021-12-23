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