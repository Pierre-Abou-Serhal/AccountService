pipeline {
    agent any

    environment {
		KUBECONFIG_PATH = "${env.WORKSPACE}/kubeconfig"
        DEPLOYMENT_YAML_PATH = "${env.WORKSPACE}/k8s/deployment.yaml"
        SERVICE_YAML_PATH = "${env.WORKSPACE}/k8s/service.yaml"
        IMAGE_NAME = "pierreas/accountservice-v1"
        IMAGE_TAG = "1.0.0"  // Using Jenkins build number for versioning
        DOCKER_HUB_CREDENTIALS = 'docker-hub-credentials-id'  // Jenkins Docker Hub credentials ID
    }

    stages {

        stage('Build Docker Image') {
            steps {
                script {
                    echo "Building Docker image..."
                    powershell '''
                        docker build -t $env:IMAGE_NAME:$env:IMAGE_TAG .
                    '''
                }
            }
        }

        stage('Docker Login And Push To DockerHub') {
            steps {
                withCredentials([usernamePassword(credentialsId: "$DOCKER_HUB_CREDENTIALS", usernameVariable: 'DOCKER_USER', passwordVariable: 'DOCKER_PASSWORD')]) {
                    script {
                        echo "Logging into Docker..."
                        powershell '''
                            docker login -u $env:DOCKER_USER -p $env:DOCKER_PASSWORD
							
							docker push $env:IMAGE_NAME:$env:IMAGE_TAG
                        '''
                    }
                }
            }
        }

        stage('Set KUBECONFIG') {
            steps {
                script {
                    echo "Setting KUBECONFIG environment variable..."
                    powershell '''
                        $env:KUBECONFIG = "$env:KUBECONFIG_PATH"
                    '''
                }
            }
        }

        stage('Apply Kubernetes Deployment') {
            steps {
                script {
                    echo "Applying Kubernetes Deployment..."
                    powershell '''
                        kubectl apply -f $env:DEPLOYMENT_YAML_PATH
                    '''
                }
            }
        }

        stage('Apply Kubernetes Service') {
            steps {
                script {
                    echo "Applying Kubernetes Service..."
                    powershell '''
                        kubectl apply -f $env:SERVICE_YAML_PATH
                    '''
                }
            }
        }
    }
}
