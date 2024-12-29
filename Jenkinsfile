pipeline {
    agent {
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:9.0'
        }
    }

    environment {
        DOCKER_IMAGE = "accountservice"
        REGISTRY = "localhost:5000" // Local Docker registry
    }

    stages {
        stage('Clone') {
            steps {
                git(branch: 'main', url: 'file:///var/jenkins_home/workspace/accountservice')
            }
        }

        stage('Restore & Build') {
            steps {
                echo 'Restoring and Building...'
                dir('workspace/accountservice') {
                    sh 'dotnet restore'
                    sh 'dotnet build --configuration Release'
                }
            }
        }

        stage('Run Unit Tests') {
            steps {
                echo 'Running Unit Tests...'
                dir('workspace/accountservice') {
                    sh 'dotnet test --no-build --verbosity normal'
                }
            }
        }

        stage('Publish') {
            steps {
                echo 'Publishing...'
                dir('workspace/accountservice') {
                    sh 'dotnet publish --configuration Release --output ./publish'
                }
            }
        }

        stage('Build Docker Image') {
            steps {
                echo 'Building Docker image...'
                script {
                    dir('workspace/accountservice') {
                        docker.build("${DOCKER_IMAGE}:${env.BUILD_ID}", './publish')
                    }
                }
            }
        }

        stage('Push Docker Image') {
            steps {
                echo 'Pushing Docker image to local registry...'
                script {
                    docker.withRegistry("http://${REGISTRY}", '') {
                        docker.image("${DOCKER_IMAGE}:${env.BUILD_ID}").push('latest')
                    }
                }
            }
        }

        stage('Deploy to Kubernetes') {
            steps {
                echo 'Deploying to Kubernetes...'
                script {
                    dir('workspace/accountservice') {
                        withCredentials([file(credentialsId: 'kubeconfig-credentials-id', variable: 'KUBECONFIG')]) {
                            sh 'kubectl apply -f k8s/deployment.yaml'
                        }
                    }
                }
            }
        }
    }

    post {
        always {
            cleanWs()
        }
    }
}
