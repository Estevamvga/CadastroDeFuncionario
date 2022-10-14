/*
 Navicat Premium Data Transfer

 Source Server         : Servidor
 Source Server Type    : MySQL
 Source Server Version : 100314
 Source Host           : 192.168.0.151:3306
 Source Schema         : sortee08_projetocli

 Target Server Type    : MySQL
 Target Server Version : 100314
 File Encoding         : 65001

 Date: 05/10/2022 18:19:13
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for porteiros
-- ----------------------------
DROP TABLE IF EXISTS `porteiros`;
CREATE TABLE `porteiros`  (
  `id_porteiro` int NOT NULL AUTO_INCREMENT,
  `nome_porteiro` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `cpf` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `telefone` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`id_porteiro`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 20 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of porteiros
-- ----------------------------

SET FOREIGN_KEY_CHECKS = 1;
