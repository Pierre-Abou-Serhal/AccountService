pipeline {
    agent any

    environment {
        DOCKER_IMAGE_NAME = 'accountservice-v1'
        DOCKER_IMAGE_TAG = 'latest'
        KUBECONFIG_PATH = 'D:/Repos/AccountService/kubeconfig.yaml'
        DEPLOYMENT_YAML_PATH = 'D:/Repos/AccountService/k8s/deployment.yaml'
        SERVICE_YAML_PATH = 'D:/Repos/AccountService/k8s/service.yaml'
    }

    stages {
        stage('Setup Minikube Docker Daemon') {
            steps {
				withEnv(["KUBECONFIG=${KUBECONFIG_PATH}"]) {
					powershell '''
					# Switch to Minikube's Docker daemon
					& minikube docker-env | Invoke-Expression

					# Log the value of DOCKER_TLS_VERIFY
					Write-Host "DOCKER_TLS_VERIFY is set to: $env:DOCKER_TLS_VERIFY"
					Write-Host "DOCKER_HOST: $env:DOCKER_HOST"
					Write-Host "DOCKER_CERT_PATH: $env:DOCKER_CERT_PATH"

					# Set DOCKER_TLS_VERIFY to 0
					$env:DOCKER_TLS_VERIFY = "0"
					$env:DOCKER_CERT_PATH = "C:/Users/Pierre A.S/.minikube/certs"

					# Log the value of DOCKER_TLS_VERIFY
					Write-Host "DOCKER_TLS_VERIFY is set to: $env:DOCKER_TLS_VERIFY"
					Write-Host "DOCKER_HOST: $env:DOCKER_HOST"
					Write-Host "DOCKER_CERT_PATH: $env:DOCKER_CERT_PATH"

					# Test connectivity to Minikube Docker daemon
					Write-Host "Testing Docker connectivity..."
					docker info

					# Build the Docker image
					docker build -t $env:DOCKER_IMAGE_NAME:$env:DOCKER_IMAGE_TAG .
					'''
				}
            }
        }

        stage('Deploy to Kubernetes') {
            steps {
                script {
                    withEnv(["KUBECONFIG=${KUBECONFIG_PATH}"]) {
                        bat "kubectl apply -f ${DEPLOYMENT_YAML_PATH}"
                        bat "kubectl apply -f ${SERVICE_YAML_PATH}"
                    }
                }
            }
        }
    }
}
