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
		stage('Set Docker Environment') {
            steps {
                script {
                    // Run minikube docker-env to capture the environment variables
                    def dockerEnv = bat(script: "\"${env.MINIKUBE_PATH}\" -p minikube docker-env", returnStdout: true).trim()

                    // Set the environment variables using a PowerShell command
                    bat """
                    powershell -Command \
                    '$dockerEnv | foreach { \$env:\$_.Split("=")[0] = \$_.Split("=")[1] }'
                    """
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
