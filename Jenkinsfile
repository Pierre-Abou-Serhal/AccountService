pipeline {
    agent any


    environment {
        DOCKER_IMAGE_NAME = 'accountservice-v1'
        DOCKER_IMAGE_TAG = 'latest'
        KUBECONFIG_PATH = 'D:/Repos/AccountService/kubeconfig.yaml'
        DEPLOYMENT_YAML_PATH = 'D:/Repos/AccountService/k8s/deployment.yaml'
        SERVICE_YAML_PATH = 'D:/Repos/AccountService/k8s/service.yaml'
        MINIKUBE_PATH = 'D:\\Opp\\Minikube\\minikube.EXE'  // Change to your actual minikube installation path
    }

    stages {
		stage('Set Docker Environment') {
            steps {
                script {
                    // Use the MINIKUBE_PATH environment variable to run the minikube command
                    bat "\"${env.MINIKUBE_PATH}\" -p minikube docker-env --shell powershell | powershell -Command \"& {$(Get-Content)}\""
                }
            }
        }
	
        stage('Build Docker Image') {
            steps {
                script {
					// Build Docker image
                    bat "docker build -t ${DOCKER_IMAGE_NAME}:${DOCKER_IMAGE_TAG} ."
                }
            }
        }

        stage('Run Tests') {
            steps {
                script {
                    // Run your tests
                    bat "dotnet test D:/Repos/AccountService/AccountService.sln"
                }
            }
        }

        stage('Deploy to Kubernetes') {
            steps {
                script {
                    // Set the Kubernetes context
                    withEnv(["KUBECONFIG=${KUBECONFIG_PATH}"]) {
                        // Apply the deployment YAML file
                        bat "kubectl apply -f ${DEPLOYMENT_YAML_PATH}"

                        // Apply the service YAML file
                        bat "kubectl apply -f ${SERVICE_YAML_PATH}"
                    }
                }
            }
        }
    }
}
