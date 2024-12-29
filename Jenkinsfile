pipeline {
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
                    sh 'docker --version'
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
