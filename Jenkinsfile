pipeline {
    agent any

    environment {
        DOCKER_IMAGE_NAME = 'accountservice-v1'  // Adjust the image name
        DOCKER_IMAGE_TAG = 'latest'
        LOCAL_REGISTRY = 'localhost:5000'  // Local Docker registry
        K8S_NAMESPACE = 'default'  // Adjust your Kubernetes namespace
        KUBECONFIG_PATH = 'D:/Repos/AccountService/kubeconfig.yaml'
        DEPLOYMENT_YAML_PATH = 'D:/Repos/AccountService/k8s/deployment.yaml'  // Path to your deployment YAML file
        SERVICE_YAML_PATH = 'D:/Repos/AccountService/k8s/service.yaml'  // Path to your service YAML file
    }

    stages {
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
                    // Run your tests (adjust to your specific testing framework)
                    bat "dotnet test D:/Repos/AccountService/AccountService.sln"
                }
            }
        }

        stage('Push Image to Local Docker Registry') {
            steps {
                script {
                    // Ensure the local Docker registry is running
                    bat "docker run -d -p 5000:5000 --name registry registry:2 || echo 'Registry already running'"

                    // Tag the image for local registry
                    bat "docker tag ${DOCKER_IMAGE_NAME}:${DOCKER_IMAGE_TAG} ${LOCAL_REGISTRY}/${DOCKER_IMAGE_NAME}:${DOCKER_IMAGE_TAG}"

                    // Push the image to local Docker registry
                    bat "docker push ${LOCAL_REGISTRY}/${DOCKER_IMAGE_NAME}:${DOCKER_IMAGE_TAG}"
                }
            }
        }

        stage('Deploy to Kubernetes') {
            steps {
                script {
                    // Set the Kubernetes context (if needed)
                    withEnv(["KUBECONFIG=${KUBECONFIG_PATH}"]) {
                        // Apply the deployment YAML file
                        bat "kubectl apply -f ${DEPLOYMENT_YAML_PATH} --namespace=${K8S_NAMESPACE}"

                        // Apply the service YAML file
                        bat "kubectl apply -f ${SERVICE_YAML_PATH} --namespace=${K8S_NAMESPACE}"

                        // Deploy the Docker image to your local Kubernetes cluster
                        bat "kubectl set image deployment/accountservice-deployment accountservice=${LOCAL_REGISTRY}/${DOCKER_IMAGE_NAME}:${DOCKER_IMAGE_TAG} --namespace=${K8S_NAMESPACE}"
                    }
                }
            }
        }
    }

    post {
        always {
            // Clean up local Docker registry container after use
            bat "docker stop registry && docker rm registry"
        }
    }
}
