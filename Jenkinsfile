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
                powershell '''
                & minikube docker-env | Invoke-Expression
				
				$env:DOCKER_TLS_VERIFY = "0"
				
				docker build -t $env:DOCKER_IMAGE_NAME:$env:DOCKER_IMAGE_TAG .
                '''
            }
        }

        stage('Run Tests') {
            steps {
                script {
                    bat "dotnet test D:/Repos/AccountService/AccountService.sln"
                }
            }
        }

        stage('Setup Kubernetes Context') {
            steps {
                script {
                    bat "minikube update-context"
                }
            }
        }

        stage('Debug Kubernetes API') {
            steps {
                script {
                    withEnv(["KUBECONFIG=${KUBECONFIG_PATH}"]) {
                        bat "kubectl cluster-info"
                        bat "kubectl get nodes"
                    }
                }
            }
        }

        stage('Deploy to Kubernetes') {
            steps {
                script {
                    withEnv(["KUBECONFIG=${KUBECONFIG_PATH}"]) {
                        bat "kubectl apply -f ${DEPLOYMENT_YAML_PATH} --validate=false"
                        bat "kubectl apply -f ${SERVICE_YAML_PATH} --validate=false"
                    }
                }
            }
        }
    }
}
