pipeline {
    agent any

    environment {
        DOCKER_IMAGE_NAME = 'accountservice-v1'
        DOCKER_IMAGE_TAG = 'latest'
        HOST_IP = 'localhost'  // Replace with your host machine's IP address
        LOCAL_REGISTRY = "${HOST_IP}:5000"
        K8S_NAMESPACE = 'default'
        KUBECONFIG_PATH = 'D:/Repos/AccountService/kubeconfig.yaml'
        DEPLOYMENT_YAML_PATH = 'D:/Repos/AccountService/k8s/deployment.yaml'
        SERVICE_YAML_PATH = 'D:/Repos/AccountService/k8s/service.yaml'
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
                    // Run your tests
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
                    // Set the Kubernetes context
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
