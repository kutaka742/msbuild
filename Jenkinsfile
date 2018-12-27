#!/usr/bin/env groovy

// �P�̃e�X�g���s�u�����`(develop/�`�̂ݎ��{)
def TARGET_BRANCH = 'develop'

pipeline {

    //���s�m�[�h��ݒ�
    agent{
        label 'master'
    }

    //����W���u�̕��s���s�s��(���s���s�\�Ƃ���Ȃ�option���ƍ폜)
    options{
        disableConcurrentBuilds()
    }

    stages{
        //���[�N�X�y�[�X�N���A
        stage ('Clean') {
            steps{
                script{
                    cleanWs()
                }
            }
        }

        //�`�F�b�N�A�E�g
        stage('Checkout'){
            steps{
                script{
                    //�`�F�b�N�A�E�g
                    checkout scm
                }
            }
        }

        //�r���h
        stage('Build'){
            steps{
                script{
                    echo "�r���h���s"
                    bat "\"C:\\Program Files\\MSBuild\\14.0\\Bin\\MSBuild.exe\" msbuild-sample.sln /t:Rebuild /p:Configuration=Release;Platform=\"Any CPU\" /l:FileLogger,Microsoft.Build.Engine;logfile=rebuild.log"
                }
            }
        }

        //�P�̃e�X�g
        stage ('Test') {
            when{
                allOf{expression {return env.BRANCH_NAME.contains("${TARGET_BRANCH}")}}
            }
            steps {
                script {
                    try {
                        echo "�e�X�g���s"
                        bat "\"C:\\Program Files\\Microsoft Visual Studio 14.0\\Common7\\IDE\\MSTest.exe\" /resultsfile:TestResults.xrt /noisolation /testcontainer:sample.Tests\\bin\\Release\\sample.Tests.dll"
                    } catch (Exception e) {
                        echo "�P�̃e�X�g�G���["
                    } finally {
                        echo "�e�X�g���ʏW�v"
                        xunit([MSTest(deleteOutputFiles: true, failIfNotNew: true, pattern: 'TestResults.xrt', skipNoTestFiles: false, stopProcessingIfError: true)])
                    }
                }
            }
        }
    }

    post{
        always{
            //���ʕ��ۑ�
            archiveArtifacts artifacts: '**/sample/bin/**/*', fingerprint: true
            archiveArtifacts artifacts: '**/rebuild.log'

            //���[�N�X�y�[�X�N���A
            cleanWs()
        }
        failure{
            echo "�G���["
        }
        success{
            echo "����"
        }
        unstable{
            echo "�s����"
        }
    }
}