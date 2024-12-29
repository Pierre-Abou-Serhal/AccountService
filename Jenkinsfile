pipeline {
    agent any

    environment {
        KUBECONFIG_PATH = 'D:/Repos/AccountService/kubeconfig.yaml'
        DEPLOYMENT_YAML_PATH = 'D:/Repos/AccountService/k8s/deployment.yaml'
        SERVICE_YAML_PATH = 'D:/Repos/AccountService/k8s/service.yaml'
        IMAGE_NAME = 'accountservice-v1'
        IMAGE_TAG = "1.0.0-${env.BUILD_NUMBER}"  // Using Jenkins build number for versioning
        DOCKER_HUB_CREDENTIALS = 'docker-hub-credentials-id'  // Jenkins Docker Hub credentials ID
    }

    stages {
        stage('Setup Minikube Docker Daemon') {
            steps {
                withCredentials([usernamePassword(credentialsId: "$DOCKER_HUB_CREDENTIALS", usernameVariable: 'DOCKER_USER', passwordVariable: 'DOCKER_PASSWORD')]) {
                    powershell '''
                        Write-Host "Testing Docker connectivity..."
                        docker info
                        
                        # Docker Login using injected credentials
                        docker login -u $env:DOCKER_USER -p $env:DOCKER_PASSWORD

                        # Build Docker Image
                        docker build -t $env:IMAGE_NAME:$env:IMAGE_TAG .

                        # Show Docker images
                        docker images

                        # Push Docker Image (if required)
                        docker push $env:DOCKER_USER/$env:IMAGE_NAME:$env:IMAGE_TAG

                        # Apply Kubernetes Deployment and Service
                        kubectl apply -f $env:DEPLOYMENT_YAML_PATH
                        kubectl apply -f $env:SERVICE_YAML_PATH
                    '''
                }
            }
        }
    }
}
