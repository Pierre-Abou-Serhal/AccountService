pipeline {
    agent any

    environment {
        KUBECONFIG_PATH = 'D:/Repos/AccountService/kubeconfig.yaml'
        DEPLOYMENT_YAML_PATH = 'D:/Repos/AccountService/k8s/deployment.yaml'
        SERVICE_YAML_PATH = 'D:/Repos/AccountService/k8s/service.yaml'
    }

    stages {
        stage('Setup Minikube Docker Daemon') {
            steps {
				withEnv(["KUBECONFIG=${KUBECONFIG_PATH}"]) {
					powershell '''

					Write-Host "Testing Docker connectivity..."
					docker info

					# Build the Docker image
					docker build -t accountservice-v1:1.0.0 .
					
					docker images
					
					kubectl apply -f $env:DEPLOYMENT_YAML_PATH
					kubectl apply -f $env:SERVICE_YAML_PATH
					'''
				}
            }
        }
    }
}
