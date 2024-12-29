pipeline {
    agent any

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
                echo 'Building...'
                sh 'dotnet restore'
                sh 'dotnet build --configuration Release'
            }
        }
		
		stage('Run Unit Tests') {
            steps {
                echo 'Testing...'
                sh 'dotnet test --no-build --verbosity normal'
            }
        }
		
		stage('Publish') {
            steps {
                echo 'Publishing...'
                sh 'dotnet publish --configuration Release --output ./publish'
            }
        }
		
		stage('Build Docker Image') {
            steps {
                echo 'Building Docker image...'
                script {
                    docker.build("${DOCKER_IMAGE}:${env.BUILD_ID}", './publish')
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
                    withCredentials([file(credentialsId: 'kubeconfig-credentials-id', variable: 'KUBECONFIG')]) {
                        sh 'kubectl apply -f k8s/deployment.yaml'
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