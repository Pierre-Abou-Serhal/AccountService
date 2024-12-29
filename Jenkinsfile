pipeline {
	agent any

    environment {
        DOCKER_IMAGE_NAME = 'accountservice-v1'  // Adjust the image name
        DOCKER_IMAGE_TAG = 'latest'
        K8S_NAMESPACE = 'default'  // Adjust your Kubernetes namespace
        KUBECONFIG_PATH = 'D:/Repos/AccountService/kubeconfig.yaml'
    }

    stages {
         stage('Build Docker Image') {
            steps {
                script {
                    // Build Docker image
                    bat 'docker build -t ${DOCKER_IMAGE_NAME}:${DOCKER_IMAGE_TAG} .'
                }
            }
        }
		
		stage('Run Tests') {
            steps {
                script {
                    // Run your tests (adjust to your specific testing framework)
                    bat 'dotnet test D:/Repos/AccountService/AccountService.sln'
                }
            }
        }
		
		stage('Push Image to Local Docker Registry') {
            steps {
                script {
                    // Ensure the local Docker registry is running
                    bat 'docker run -d -p 5000:5000 --name registry registry:2'

                    // Tag the image for local registry
                    bat 'docker tag ${DOCKER_IMAGE_NAME}:${DOCKER_IMAGE_TAG} localhost:5000/${DOCKER_IMAGE_NAME}:${DOCKER_IMAGE_TAG}'

                    // Push the image to local Docker registry
                    bat 'docker push localhost:5000/${DOCKER_IMAGE_NAME}:${DOCKER_IMAGE_TAG}'
                }
            }
        }

        stage('Deploy to Kubernetes') {
            steps {
                script {
                    // Set the Kubernetes context (if needed)
                    withEnv(["KUBECONFIG=${KUBECONFIG_PATH}"]) {
                        // Deploy the Docker image to your local Kubernetes cluster
                        bat 'kubectl set image deployment/accountservice=localhost:5000/${DOCKER_IMAGE_NAME}:${DOCKER_IMAGE_TAG} --namespace=${K8S_NAMESPACE}'
                    }
                }
            }
        }
    }
	
	post {
        always {
            // Clean up local Docker registry container after use
            bat 'docker stop registry && docker rm registry'
        }
    }
}
